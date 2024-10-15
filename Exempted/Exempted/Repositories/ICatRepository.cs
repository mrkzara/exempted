using Exempted.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Exempted.Repositories
{
    public interface ICatRepository
    {
        Task<IEnumerable<Cat>> GetAllCatsAsync();
        Task<Cat> GetCatByIdAsync(int id);
        Task<int> AddCatAsync(Cat cat);
        Task<int> UpdateCatAsync(Cat cat);
        Task<int> DeleteCatAsync(int id);
    }
}
