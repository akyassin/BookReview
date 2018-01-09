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
using SPCF.ViewModels;

namespace SPCF.Controllers
{
    public class CommentsController : Controller
    {
        private SPContext db = new SPContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Books).Include(c => c.UserProfiles);
            return View(comments.ToList());
        }


        // GET: Comments/Create
        public ActionResult Create()
        {
            var users = db.Users.ToList();
            var books = db.Books.ToList();

            var models = new ViewModels.Comments.Create(users, books);
            return View(models);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.Comments.Create model)
        {
            var comment = new Comments();

            comment.Created = DateTime.UtcNow.Ticks;
            comment.BookID = model.selecedBookId;
            comment.UserID = model.selectedUserId;
            comment.Comment = model.Comment;

            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int Id)
        {
            var comment = db.Comments.Find(Id);

            var books = db.Books.ToList();
            var users = db.Users.ToList();
            var model = new ViewModels.Comments.Edit(users, books, comment);

            return View(model);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.Comments.Edit model)
        {
            var comment = db.Comments.Find(model.Id);

            comment.Modified = DateTime.UtcNow.Ticks;
            comment.Comment = model.Comment;
            comment.BookID = model.selectedBookId;
            comment.UserID = model.selectedUserId;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = db.Comments.Find(id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var model = new ViewModels.Comments.Delete(comment);

            return View(model);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ViewModels.Comments.Delete model)
        {
            var comment = db.Comments.Find(model.Id);

            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
