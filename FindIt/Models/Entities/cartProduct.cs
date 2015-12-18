using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities {
    public class CartProduct {
        public virtual Product Product { get; set; }
        public int Qty { get; set; }
    }
}