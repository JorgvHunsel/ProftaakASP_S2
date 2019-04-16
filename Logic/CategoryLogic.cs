using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class CategoryLogic
    {
        private readonly ICategoryContext _category;

        public CategoryLogic(ICategoryContext category)
        {
            _category = category;
        }

        public List<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
        }

       
    }
}
