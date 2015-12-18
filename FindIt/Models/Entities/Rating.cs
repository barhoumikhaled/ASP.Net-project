using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}