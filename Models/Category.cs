using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Classes
{
    public class Category
    {

        public int CategoryID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category(string categoryName)
        {
            Name = categoryName;
        }

        public Category(int categoryID, string categoryName, string categoryDescription)
        {
            this.CategoryID = categoryID;
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
