using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {

        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category(string categoryName)
        {
            Name = categoryName;
        }

        public Category(int categoryId)
        {
            CategoryId = categoryId;
        }

        public Category(int categoryID, string categoryName, string categoryDescription)
        {
            this.CategoryId = categoryID;
            this.Name = categoryName;
            this.Description = categoryDescription;
        }

        public void AddCategory(string name)
        {
            //Add new Category
        }

        public void EditCategory(Category c)
        {
            //Edit c.Description
        }
        
        public void DeleteCategory(Category c)
        {
            //Delete the category tha's been given as parameter
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
