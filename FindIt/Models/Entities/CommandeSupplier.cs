using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class CommandeSupplier
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public string EmployeeId { get; set; }
        public virtual ApplicationUser Employee { get; set; }
        public virtual ICollection<EntryCommandeSupplier> EntryCommandeSupplier { get; set; }
    }
}