using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace api.controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;


        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentRepository.GetAllAsync();
                var CommentDtos = comments.Select(s => s.TocommentDto());
                return Ok(CommentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound("No items found");
                }
                var CommentDto = comment.TocommentDto();
                return Ok(CommentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // POST: api/comments/{stockId}
        [HttpPost("{StockId}")]
        public async Task<IActionResult> Create([FromRoute] int StockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _stockRepository.StockExists(StockId))
                {
                    return NotFound($"Stock with ID {StockId} not found");
                }

                var commentModel = commentDto.TocommentFromCreate(StockId);
                await _commentRepository.CreateAsync(commentModel);
                return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.TocommentDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] updateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comment = await _commentRepository.UpdateAsync(id, updateDto.TocommentFromUpdate());

                if (comment == null)
                {
                    return NotFound("Comment not found");
                }

                return Ok(comment.TocommentDto());

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _commentRepository.DeleteAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                return Ok("Item has been deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}