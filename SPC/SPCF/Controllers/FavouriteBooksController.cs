using System;
using System.Net;
using System.Linq;
using SPCF.Models;
using System.Web.Mvc;
using DataBase.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity;


namespace SPCF.Controllers
{
    public class FavouriteBooksController : Controller
    {
        private SPContext db = new SPContext();

        // GET: FavouriteBooks
        public ActionResult Index()
        {
            var favouriteBooks = db.FavouriteBooks.Include(f => f.Books).Include(f => f.UserProfiles);
            return View(favouriteBooks.ToList());
        }


        // GET: FavouriteBooks/Create
        public ActionResult Create()
        {
            var users = db.Users.ToList();
            var books = db.Books.ToList();
            var favbook = new FavouriteBooks();

            var model = new ViewModels.FavouriteBook.FavouriteGeneral(books, users, favbook);

            return View(model);
        }

        // POST: FavouriteBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.FavouriteBook.FavouriteGeneral model)
        {
            if (ModelState.IsValid)
            {
                var favouriteBook = new FavouriteBooks();

                favouriteBook.Created = DateTime.UtcNow.Ticks;
                favouriteBook.BookID = model.selectedBookId;
                favouriteBook.UserID = model.selectedUserId;

                db.FavouriteBooks.Add(favouriteBook);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        //Add Book to Favourite Table
        [HttpGet]
        public ActionResult AddToFav(int? Id)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(Id);
                var userID = User.Identity.GetUserId();

                var favBook = new FavouriteBooks();
                favBook.BookID = book.BookID;
                favBook.UserID = userID;


                db.FavouriteBooks.Add(favBook);

                db.SaveChanges();
                return RedirectToAction("Index","Books");
            }

            return View();
        }

        //Remove Book to Favourite Table
        [HttpGet]
        public ActionResult RemFromoFav(int Id)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                var favBook = db.FavouriteBooks.FirstOrDefault(a => (a.UserID == userID && a.BookID == Id));


                db.FavouriteBooks.Remove(favBook);

                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }

            return View();
        }

        //Add Book to Favourite Table (Details Page)
        [HttpGet]
        public ActionResult AddToFavDetails(int? Id)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(Id);
                var userID = User.Identity.GetUserId();

                var favBook = new FavouriteBooks();
                favBook.BookID = book.BookID;
                favBook.UserID = userID;


                db.FavouriteBooks.Add(favBook);

                db.SaveChanges();
                return RedirectToAction("Details", "Books", new { Id = Id });
            }

            return View();
        }

        //Remove Book to Favourite Table (Details Page)
        [HttpGet]
        public ActionResult RemFromoFavDetails(int? Id)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                var favBook = db.FavouriteBooks.FirstOrDefault(a => (a.UserID == userID && a.BookID == Id));


                db.FavouriteBooks.Remove(favBook);

                db.SaveChanges();
                return RedirectToAction("Details","Books", new { Id = Id });
            }

            return View();
        }

        // GET: FavouriteBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FavouriteBooks favouriteBooks = db.FavouriteBooks.Find(id);

            if (favouriteBooks == null)
            {
                return HttpNotFound();
            }

            var users = db.Users.ToList();
            var books = db.Books.ToList();


            var model = new ViewModels.FavouriteBook.FavouriteGeneral(books, users, favouriteBooks);

            return View(model);
        }

        // POST: FavouriteBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.FavouriteBook.FavouriteGeneral model)
        {
            if (ModelState.IsValid)
            {
                var favbook = db.FavouriteBooks.Find(model.Id);

                favbook.Modified = DateTime.UtcNow.Ticks;
                favbook.BookID = model.selectedBookId;
                favbook.UserID = model.selectedUserId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: FavouriteBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavouriteBooks favouriteBooks = db.FavouriteBooks.Find(id);
            if (favouriteBooks == null)
            {
                return HttpNotFound();
            }

            var model = new ViewModels.FavouriteBook.FavouriteGeneral(favouriteBooks);
            return View(model);
        }

        // POST: FavouriteBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ViewModels.FavouriteBook.FavouriteGeneral model)
        {
            FavouriteBooks favouriteBooks = db.FavouriteBooks.Find(model.Id);

            db.FavouriteBooks.Remove(favouriteBooks);
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
