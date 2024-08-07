using System.Collections.Generic;
using System.Threading.Tasks;
using api.Helpers;
using Models;

namespace api.Repositories
{
    public interface IStockRepository
    {
        // Task<IEnumerable<Stock>> GetAllAsync();
        Task<IEnumerable<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> AddAsync(Stock stock);
        Task<Stock> UpdateAsync(Stock stock);
        Task<bool> DeleteAsync(int id);
        Task<bool> StockExists(int id);

    }
}
