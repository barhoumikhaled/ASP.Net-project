using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using FindIt.Models.Entities;

namespace FindIt.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "un minimum de 4 caratere pour votre produit")]
        public string Name { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "un minimum de 15 caratere pour votre produit")]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000000000000000, ErrorMessage = "un double compris entre 0 et 1000000000000000 ")]
        public double Price { get; set; }
        public String Photo { get; set; }
        public int Qty { get; set; }
        public virtual ICollection<Keyword> Keyword { get; set; }
        public virtual ICollection<Supplier> Supplier { get; set; }

        public virtual SubCategorie SubCategorie { get; set; }
        public Nullable<int> SubCategorieID { get; set; }


        public virtual ICollection<File> Files { get; set; }

        [NotMapped]
        public List<int> SelectedItemKeyWordIds { get; set; }

        [NotMapped]
        public MultiSelectList KeyWordItems { get; set; }

        [NotMapped]
        public List<int> SelectedItemSupplierIds { get; set; }

        [NotMapped]
        public MultiSelectList SupplierItems { get; set; }
    }
}