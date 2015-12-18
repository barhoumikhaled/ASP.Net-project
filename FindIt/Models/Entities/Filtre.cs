using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities {
    public class Filtre {
        public String Username { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
    }
}