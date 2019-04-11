using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Logic
{
    public class CategoryLogic
    {
        CategoryRepository categoryRepo = new CategoryRepository();

        public List<Category> GetAllCategories()
        {
            return categoryRepo.GetAllCategories();
        }

       
    }
}
