using Microsoft.AspNet.Identity;
using proiect.Models;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.IO;
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
          
            var photos = db.dbPhotos.Include("Photos").Include("User").Include("Category").Include("Album");


            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }
            ViewBag.Photos = photos;
            return View();
        }
        [NonAction]
        public IEnumerable<Comment> GetAllComments(int PhotoId)
        {
            // generam o lista goala             
            var commentList = new List<Comment>();

            // Extragem toate categoriile din baza de date            
            var comments = from com in db.dbComments
                           where com.PhotoId==PhotoId
                           select com;

            commentList = comments.ToList();

            

            // returnam lista de categorii            
            return commentList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista goala             
            var selectList = new List<SelectListItem>();

            // Extragem toate categoriile din baza de date            
            var categories = from cat in db.dbCategories
                             select cat;

            // iteram prin categorii             
            foreach (var category in categories)
            {
                // Adaugam in lista elementele necesare pentru dropdown                 
                selectList.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName.ToString()
                }
                                );
            }

            // returnam lista de categorii            
            return selectList;
        }
        [NonAction]
        public IEnumerable<SelectListItem> GetAllAlbums()
        {
            // generam o lista goala             
            var selectList = new List<SelectListItem>();

            // Extragem toate categoriile din baza de date            
            var albums = from alb in db.dbAlbums
                            where alb.UserId== User.Identity.GetUserId()
                         select alb;

            // iteram prin categorii             
            foreach (var alb in albums)
            {
                // Adaugam in lista elementele necesare pentru dropdown                 
                selectList.Add(new SelectListItem
                {
                    Value = alb.AlbumId.ToString(),
                    Text = alb.AlbumTitle.ToString()
                }
                                );
            }

            // returnam lista de categorii            
            return selectList;
        }
<<<<<<< HEAD
        
        /*public ActionResult Show(int id)
=======
        public ActionResult Show(int id)
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
        {
            Photo photo = db.dbPhotos.Find(id);
            ViewBag.Photo = photo;
            ViewBag.Category = photo.Category;
            ViewBag.Album = photo.Album;
            ViewBag.Comments = GetAllComments(id);


            //ViewBag.afisareButoane = false;
            /*if (User.IsInRole("Editor") || User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }*/

            //ViewBag.esteAdmin = User.IsInRole("Administrator");
<<<<<<< HEAD
            //ViewBag.utilizatorCurent = User.Identity.GetUserId();
            //return View(photo);
        //}

        [HttpGet]
        public ActionResult Show(int id)
        {
            Photo photo = db.dbPhotos.Find(id);
            return View(photo);
        }
        
=======
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
            return View(photo);
        }

>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
        public ActionResult Edit(int id)
        {
            Photo photo = db.dbPhotos.Find(id);
            ViewBag.Photo = photo;
            photo.Categories = GetAllCategories();

            //verifica daca userul logat poate face aceste operatii
            if (photo.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(photo);
            }
            else
            {
                TempData["message"] = "You don`t have the right to do this";
                return RedirectToAction("Index");
            }


        }

        [HttpPut]

        public ActionResult Edit(int id, Photo requestPhoto)
        {
            requestPhoto.Categories = GetAllCategories();
            requestPhoto.Albums = GetAllAlbums();
            try
            {
                if (ModelState.IsValid)
                {
                    Photo photo = db.dbPhotos.Find(id);
                    if (TryUpdateModel(photo))
                    {
                        photo.Description = requestPhoto.Description;
                        photo.CategoryId = requestPhoto.CategoryId;
                        photo.AlbumId = requestPhoto.AlbumId;

                        db.SaveChanges();
                        TempData["message"] = "Photo was succesfully modified";
                    }
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestPhoto);
                }
            }

            catch (Exception e)
            {
                return View(requestPhoto);
            }
        }

        [HttpDelete]

        public ActionResult Delete(int id)
        {
            Photo photo = db.dbPhotos.Find(id);
            db.dbPhotos.Remove(photo);
            db.SaveChanges();
            TempData["message"] = "Photo was succesfully deleted";

            return RedirectToAction("Index");
        }

<<<<<<< HEAD

        [HttpPost]
        public ActionResult Add(Photo photo)
        {
            string location = Path.GetFileNameWithoutExtension(photo.ImageFile.FileName);
            string extension = Path.GetExtension(photo.ImageFile.FileName);
            string filename = location + DateTime.Now.ToString("yymmssfff") + extension;
            photo.Location = "~/Photo/" + filename;
            filename = Path.Combine(Server.MapPath("~/Photo/"), filename);
            photo.ImageFile.SaveAs(filename);
            db.dbPhotos.Add(photo); //insert
            db.SaveChanges(); //commit
            ModelState.Clear();
            return View();
        }
=======
>>>>>>> eabc07c4fc38e6bb0811fad42e0c3922a265180c
    }
}