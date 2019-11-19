using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proiect.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Add a title")]
        public string AlbumTitle { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }
    }
}