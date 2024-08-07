using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock StockModal)
        {
            return new StockDto
            {
                Id = StockModal.Id,
                Symbol = StockModal.Symbol,
                CompanyName = StockModal.CompanyName,
                Purchase = StockModal.Purchase,
                LastDiv = StockModal.LastDiv,
                Industry = StockModal.Industry,
                MarketCap = StockModal.MarketCap,
                Comments=StockModal.Comments.Select(c=>c.TocommentDto()).ToList()
            };
        }

          public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
}