using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c

namespace proiect.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        [Required(ErrorMessage = "Upload a photo")]
<<<<<<< HEAD
        [DisplayName("Upload file")]
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
        public string Location { get; set; }

        [Required(ErrorMessage = "Add a description")]
        [MaxLength(100, ErrorMessage = "Description can`t have more than 100 characters")]
        public string Description { get; set; }

        public DateTime Date { get; set; }


        //chei externe
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int AlbumId { get; set; }

        public virtual Category Category { get; set; } //datele despre categoria din care face parte
        public IEnumerable<SelectListItem> Categories { get; set; } //doar pt drop down
        public virtual ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }

        public virtual Album Album { get; set; }

        public IEnumerable<SelectListItem> Albums { get; set; }
<<<<<<< HEAD

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
    }
}