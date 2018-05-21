using Sibri.Contract.category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sibri.BLL.category.services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoryListByID(int? id, int direction);
        Task<Category> GetCategoryByID(int id);
        Task<Category> GetParentByID(int id);
    }
}
