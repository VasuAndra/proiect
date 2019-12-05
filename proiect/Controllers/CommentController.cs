using Microsoft.AspNet.Identity;
using proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult New(int PhotoId)
        {
            Comment com = new Comment();
            
            // Preluam ID-ul utilizatorului curent 
            com.UserId = User.Identity.GetUserId();

            // Adaugam id-ul pozei
            com.PhotoId = PhotoId;

            return View(com);

        }

        [HttpPost]
        public ActionResult New(Comment com)
        {
            
            try
            {
                if (ModelState.IsValid)
                {

                    db.dbComments.Add(com); //insert
                    db.SaveChanges(); //commit
                    TempData["message"] = "Comment was added.";
                    return RedirectToAction("Show","Photo",new { PhotoId=com.PhotoId});
                }
                else
                {
                    return View(com);
                }
            }
            catch (Exception e)
            {
                return View(com);
            }

        }

        public ActionResult Edit(int id)
        {
            Comment com = db.dbComments.Find(id);
            ViewBag.Comment = com;
                                                                                                                                                

            //verifica daca userul logat poate face aceste operatii
            if (com.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(com);
            }
            else
            {
                TempData["message"] ="You don`t have the right to do this!";
                return RedirectToAction("Show", "Photo", new { PhotoId = com.PhotoId });
            }


        }


        [HttpPut]
        
        public ActionResult Edit(int id, Comment requestComment)
        {
            
            try
            {
                if (ModelState.IsValid)
                {


                    Comment com = db.dbComments.Find(id);
                    if (com.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(com))
                        {
                            
                            com.Content = requestComment.Content;
                            com.Date = requestComment.Date;
                            db.SaveChanges();
                            TempData["message"] = "Comment was modified.";
                        }

                        return RedirectToAction("Show", "Photo", new { PhotoId = com.PhotoId });
                    }
                    else
                    {
                        TempData["message"] = "You don`t have the right to do this!";
                        return RedirectToAction("Show", "Photo", new { PhotoId = com.PhotoId });
                    }
                }
                else
                {
                    return View(requestComment);
                }
            }
            catch (Exception e)
            {
                return View(requestComment);
            }
        }

        [HttpDelete]
   
        public ActionResult Delete(int id)
        {
            Comment com = db.dbComments.Find(id);

            if (com.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                db.dbComments.Remove(com);
                db.SaveChanges();
                TempData["message"] = "Comments was deleted";
                return RedirectToAction("Show", "Photo", new { PhotoId = com.PhotoId });
            }
            else
            {
                TempData["message"] = "You don`t have the right";
                return RedirectToAction("Show", "Photo", new { PhotoId = com.PhotoId });
            }

        }

    }
}