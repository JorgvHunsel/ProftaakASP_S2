using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Models;

namespace Logic
{
    public class CategoryLogic
    {
        CategoryContextSQL categoryRepo = new CategoryContextSQL();

        public List<Category> GetAllCategories()
        {
            return categoryRepo.GetAllCategories();
        }

       
    }
}
