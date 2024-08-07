using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Repositories;
using api.Dtos.Stock;

using api.Dtos;
using api.Helpers;

namespace api.controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // GET: api/stocks
        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     try
        //     {
        //         var stocks = await _stockRepository.GetAllAsync();
        //         var StockDtos = stocks.Select(s => s.ToStockDto());
        //         return Ok(StockDtos);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            try
            {
                var stocks = await _stockRepository.GetAllAsync(query);
                var stockDtos = stocks.Select(s => s.ToStockDto());
                return Ok(stockDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/stocks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var stock = await _stockRepository.GetByIdAsync(id);
                if (stock == null)
                {
                    return NotFound("No items found");
                }
                var StockDto = stock.ToStockDto();
                return Ok(StockDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var stockModel = stockDto.ToStockFromCreateDto();
                var createdStock = await _stockRepository.AddAsync(stockModel);

                return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, createdStock.ToStockDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingStock = await _stockRepository.GetByIdAsync(id);

                if (existingStock == null)
                {
                    return NotFound();
                }

                // Update the existing stock entity with values from the DTO
                existingStock.Symbol = stockDto.Symbol;
                existingStock.CompanyName = stockDto.CompanyName;
                existingStock.Purchase = stockDto.Purchase;
                existingStock.LastDiv = stockDto.LastDiv;
                existingStock.Industry = stockDto.Industry;
                existingStock.MarketCap = stockDto.MarketCap;

                var updatedStock = await _stockRepository.UpdateAsync(existingStock);

                return Ok(updatedStock.ToStockDto());
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
                var result = await _stockRepository.DeleteAsync(id);

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
