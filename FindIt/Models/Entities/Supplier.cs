using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [DisplayName("Contact Ressource")]
        public string ContactRessource { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}