using proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public ActionResult Index()
        {
            //var articles = from article in db.Articles   
            //select article;   

            var categories = from categ in db.dbCategories
                             orderby categ.CategoryName
                             select categ;
            ViewBag.Categories = categories;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            return View();
        }

        public ActionResult Show(int id)
        {
            Category category = db.dbCategories.Find(id);

            var photos = from photo in db.dbPhotos
                           where photo.CategoryId == id
                           select photo;
            ViewBag.Photos = photos;

            return View(category);
        }

        public ActionResult New()
        {
            Category categorie = new Category();


            return View(categorie);

        }

        [HttpPost]

        public ActionResult New(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.dbCategories.Add(category); //insert
                    db.SaveChanges(); //commit
                    TempData["message"] = "Category was succesfully added.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(category);
                }
            }
            catch (Exception e)
            {
                return View();
            }

        }

        public ActionResult Edit(int id)
        {

            Category category = db.dbCategories.Find(id);

            return View(category);
        }

        [HttpPut]

        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = db.dbCategories.Find(id);
                    if (TryUpdateModel(category))
                    {
                        category.CategoryName = requestCategory.CategoryName;

                        db.SaveChanges();
                    }
                    TempData["message"] = "Category was succesfully modified";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestCategory);
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
            Category category = db.dbCategories.Find(id);
            db.dbCategories.Remove(category);
            db.SaveChanges();
            TempData["message"] = "Category was succesfully deleted";

            return RedirectToAction("Index");
        }


    }
}