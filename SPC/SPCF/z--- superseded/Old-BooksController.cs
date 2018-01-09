using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBase.Entities;
using SecondProjectCodeFirst.Data;
using SecondProjectCodeFirst.ViewModels;

namespace SecondProjectCodeFirst.Controllers
{
    public class BooksController : Controller
    {
        private SPContext db = new SPContext();

        // GET: Books
        public ActionResult Index()

        {
            var books = db.Books.Include(b => b.Authors).Include(b => b.Category);
            return View(books.ToList());
        }
        
        //// GET: Books/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Books books = db.Books.Find(id);
        //    if (books == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(books);
        //}

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            var book = db.Books.Find(id);
            var authors = db.Authors.Find(book.AuthorID);
            var categories = db.Category.Find(book.CategoryID);
            
            var model = new ViewModels.BookViewModel.Details(authors, categories, book);

            return View(model);
        }




        // GET: Books/Create
        public ActionResult Create()
        {
            var authors = db.Authors.ToList();
            var categories = db.Category.ToList();
            var model = new ViewModels.BookViewModel.Create(authors, categories);

            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.BookViewModel.Create model)
        {
            if (ModelState.IsValid)
            {
 
                var book = new Books();
                book.Created = DateTime.UtcNow.Ticks;
                book.BookName = model.Name;
                book.Description = model.Description;
                book.Image = ImageArray.ToByteArray(model.Upload);
                book.AuthorID = model.SelectedAuthorId;
                book.CategoryID = model.SelectedCategoryId;

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            model = new ViewModels.BookViewModel.Create(db.Authors.ToList(), db.Category.ToList());
            return View(model);
        }

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Books books = db.Books.Find(id);
        //    if (books == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var authors = db.Authors.ToList();
        //    var model = authors.Select(a => new AuthorViewModel(a));
        //    ViewBag.AuthorID = new SelectList(model, "AuthorID", "AuthorName");
        //    ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "CategoryName", books.CategoryID);
        //    return View(books);
        //}


        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BookID,Created,Modified,BookName,Description,Image,AuthorID,CategoryID")] Books books)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var ticks = DateTime.Now.Ticks;
        //        books.Created = ticks;
        //        var bookFromDb = db.Books.Find(books.BookID);
        //        DateTime date = new DateTime(ticks, DateTimeKind.Local);
        //        bookFromDb.Modified = date.Ticks;
        //        bookFromDb.BookName = bookFromDb.BookName;

        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", books.AuthorID);
        //    ViewBag.CategoryID = new SelectList(db.Category, "CategoryID", "CategoryName", books.CategoryID);
        //    return View(books);
        //}



        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            var book = db.Books.Find(id);

            var authors = db.Authors.ToList();
            var categories = db.Category.ToList();
             
            var model = new ViewModels.BookViewModel.Edit( book, categories, authors);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.BookViewModel.Edit model)
        {

            var book = new Books();

            if (ModelState.IsValid)
            {
                book.Created = DateTime.Now.Ticks;
                book.BookName = model.Name;
                book.Description = model.Discription;
                book.AuthorID = model.SelectedAuthorId;
                book.CategoryID = model.SelectedCategoryId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            var model = new ViewModels.BookViewModel.Delete(books);
            return View(model);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ViewModels.BookViewModel.Delete model)
        {
            var book = db.Books.Find(model.Id);

            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Books books = db.Books.Find(id);
        //    db.Books.Remove(books);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _UploadImage(int BookId, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                var BookFromDb = db.Books.Find(BookId);
                BookFromDb.Image = ImageArray.ToByteArray(image);

                db.SaveChanges();
                return RedirectToAction("Edit", new { id = BookId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteImage(int bookId)
        {
            var books = db.Books.Find(bookId);
            books.Image = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = books.BookID });
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
