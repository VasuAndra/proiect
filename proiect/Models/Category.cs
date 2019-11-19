using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Campul nume este obligatoriu")]
        public string CategoryName { get; set; }

        public virtual ICollection<Photo> Photo { get; set; } //parte din setarea one to many
    }
}