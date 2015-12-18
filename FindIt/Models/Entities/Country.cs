using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}