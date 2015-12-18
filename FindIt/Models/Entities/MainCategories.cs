using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class MainCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubCategorie> SubCategorie { get; set; }
    }
}