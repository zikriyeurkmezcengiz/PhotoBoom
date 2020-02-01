using PhotoBoom.Models;
using PhotoBoom.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoBoom.Controllers
{
    public class HomeController : Controller
    {
        PhotoRepository repo = new PhotoRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //GET: Home/List
        [HttpGet]
        public ActionResult List()
        {
            return View(repo.ListAllImages());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetImage(id));
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Image image)
        {
            string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            image.ImageFile.SaveAs(filename);

            repo.Add(image);

            ModelState.Clear();
            return RedirectToAction("List");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repo.GetImage(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Image image)
        {
            try
            {
                repo.DeleteImage(id);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
