using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ecrivez un commentaire avant d'envoyer")]
        public string comments { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}