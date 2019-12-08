using Microsoft.AspNet.Identity;
using proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            //var articles = from article in db.Articles   
            //select article;   
            var userId = User.Identity.GetUserId();
            var albums = from album in db.dbAlbums
                         where album.UserId == userId
                         orderby album.AlbumTitle
                         select album;
            ViewBag.Albums = albums;
            ViewBag.UserId = userId;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            return View();
        }

        public ActionResult Show(int id)
        {
            Album album = db.dbAlbums.Find(id);

            return View(album);
        }

        public ActionResult New()
        {
            Album album = new Album();


            return View(album);

        }

        [HttpPost]

        public ActionResult New(Album album)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.dbAlbums.Add(album); //insert
                    db.SaveChanges(); //commit
                    TempData["message"] = "Album was succesfully added.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(album);
                }
            }
            catch (Exception e)
            {
                return View(album);
            }

        }

        public ActionResult Edit(int id)
        {

            Album album = db.dbAlbums.Find(id);

            return View(album);
        }

        [HttpPut]

        public ActionResult Edit(int id, Album requestAlbum)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Album album = db.dbAlbums.Find(id);
                    if (TryUpdateModel(album))
                    {
                        album.AlbumTitle = requestAlbum.AlbumTitle;

                        db.SaveChanges();
                    }
                    TempData["message"] = "Album was succesfully modified";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestAlbum);
                }
            }

            catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]

        public ActionResult Delete(int id)
        {
            Album album = db.dbAlbums.Find(id);
            db.dbAlbums.Remove(album);
            db.SaveChanges();
            TempData["message"] = "Album was succesfully deleted";

            return RedirectToAction("Index");
        }
    }
}