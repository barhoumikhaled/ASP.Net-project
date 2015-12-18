using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public int No { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [NotMapped]
        public int? CountryId { get; set; }
        
        [NotMapped]
        public int? SupplierId { get; set; }

        [NotMapped]
        public string UserId { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get;set; }
    }
}