using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBase.Entities;
using SPCF.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;
using System.Text;

namespace SPCF.Controllers
{
    
    public class AuthorsController : Controller
    {
        private SPContext db = new SPContext();

        // GET: Authors
        public ActionResult Index()
        {
            TempData.Remove("LastBookId");
            return View(db.Authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors author = db.Authors.Find(id);
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new ViewModels.Author.Details(author);

            if (TempData["LastBookId"] != null)
            {
                return View("Details", model);
            }
            else
            {
                return View(model);
            }

        }


        // GET: Authors/Create
        public ActionResult Create()
        {
            var model = new ViewModels.Author.Create();

            return View(model);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.Author.Create model)
        {
            if (ModelState.IsValid)
            {
                var author = new Authors();
                author.Created = DateTime.UtcNow.Ticks;
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
                author.Description = model.Description;
                author.Image = ViewModels.ImageArray.ToByteArray(model.Upload);

                db.Authors.Add(author);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors author = db.Authors.Find(id);
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new ViewModels.Author.Edit(author);

            return View(model);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.Author.Edit model)
        {
            Authors author = db.Authors.Find(model.Id);

            author.FirstName = model.FirstName;
            author.LastName = model.LastName;
            author.Description = model.Description;
            author.Modified = DateTime.UtcNow.Ticks;

            // did the user upload an image?
            if (model.Upload != null)
            {
                author.Image = ViewModels.ImageArray.ToByteArray(model.Upload);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors author = db.Authors.Find(id);
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new ViewModels.Author.Delete(author);

            return View(model);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authors Author = db.Authors.Find(id);

            db.Authors.Remove(Author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _UploadImage(int AuthorId, HttpPostedFileBase Upload)
        {
            if (ModelState.IsValid)
            {
                var authorFromDb = db.Authors.Find(AuthorId);
                authorFromDb.Image = ViewModels.ImageArray.ToByteArray(Upload);

                db.SaveChanges();

                return RedirectToAction("Edit", new { id = AuthorId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteImage(int AuthorId)
        {
            var author = db.Authors.Find(AuthorId);
            author.Image = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = author.AuthorID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
