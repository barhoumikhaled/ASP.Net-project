using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class SubCategorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public int MainCategoriesId { get; set; }
        public virtual MainCategories MainCategories { get; set; }
    }
}