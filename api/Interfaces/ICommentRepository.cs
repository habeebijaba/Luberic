using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> CreateAsync(Comment commentModel);

        Task<Comment?> UpdateAsync(int id,Comment commentModel);

        Task<bool> DeleteAsync(int id);




        
    }
}