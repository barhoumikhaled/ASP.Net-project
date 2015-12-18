using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class EntryCommandeClient
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public int Quantity { get; set; }
        public int CommandeClientId{ get; set; }
        public virtual CommandeClient CommandeClient { get; set; }
    }
}