using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class EntryCommandeSupplier
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public int CommandeSupplierId { get; set; }
        public virtual CommandeSupplier CommandeSupplier { get; set; }
    }
}