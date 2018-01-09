using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBase.Entities;
using SPCF.Models;
using SPCF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
using System.Collections.Generic;

namespace SPCF.Controllers
{
    public class BooksController : Controller
    {
        private SPContext db = new SPContext();


        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortAuthorParameter = sortBy == "Author" ? "Author desc" : "Author";
            ViewBag.SortCategoryParameter = sortBy == "Category" ? "Category desc" : "Category";

            var books = db.Books.AsQueryable();

            if (searchBy == "Author")
            {
                books = books.Where(a => a.Authors.FirstName.Contains(search) || a.Authors.LastName.Contains(search) || search == null).Include(b => b.Authors).Include(b => b.Category);
            }
            else if (searchBy == "Category")
            {
                books = books.Where(a => a.Category.CategoryName.Contains(search) || search == null).Include(b => b.Authors).Include(b => b.Category);
            }
            else
            {
                books = books.Where(a => a.BookName.Contains(search) || search == null).Include(b => b.Authors).Include(b => b.Category);
            }

            switch (sortBy)
            {
                case "Name desc":
                books = books.OrderByDescending(x => x.BookName);
                    break;

                case "Author desc":
                    books = books.OrderByDescending(x => x.Authors.FirstName);
                    break;

                case "Author":
                    books = books.OrderBy(x => x.Authors.FirstName);
                    break;

                case "Category desc":
                    books = books.OrderByDescending(x => x.Category.CategoryName);
                    break;

                case "Category":
                    books = books.OrderBy(x => x.Category.CategoryName);
                    break;

                default:
                    books = books.OrderBy(x => x.BookName);
                    break;
            }

            return View(books.ToPagedList(page ?? 1, 4));

        }


        //// GET: Books
        //public ActionResult Index()

        //{
        //    var books = db.Books.Include(b => b.Authors).Include(b => b.Category);
        //    return View(books.ToList());
        //}



        [HttpGet]
        public ActionResult _AddComment(int id)
        {
            var book = db.Books.Find(id);   // get book by id
            var user = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var model = new ViewModels.Book.AddComment(book, user);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _AddComment(ViewModels.Book.AddComment model)
        {
            if (ModelState.IsValid)
            {
                //add comment to book, save
                var comment = new Comments();

                comment.BookID = model.BookID;
                comment.UserID = model.UserID;
                comment.Comment = model.Comment;
                comment.Created = DateTime.UtcNow.Ticks;

                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", new { Id = model.BookID });
            }
            return RedirectToAction("Details", new { Id = model.BookID });

        }

        // GET : The Total Review of The Book
        [HttpGet]
        public ActionResult _BookReview(int? Id)
        {
            var book = db.Books.Find(Id);
            var rates = db.Rates.FirstOrDefault(a => a.BookID == book.BookID) ?? null;

            if (rates != null)
            {
                var ratesSum = db.Rates.Where(a => a.BookID == book.BookID).Sum(a => (int)a.RateValue);
                var ratesCount = db.Rates.Where(a => a.BookID == book.BookID).Select(a => a.RateValue).Count();
                ViewBag.ratesAvg = ratesSum / ratesCount;
            }

            return View(rates);
        }

        // GET : The User Review for The Book
        [HttpGet]
        public ActionResult _UserReview(int? Id)
        {
            var book = db.Books.Find(Id);
            var currentUserId = User.Identity.GetUserId();

            if (User.Identity.IsAuthenticated && db.Rates.Where(a => a.BookID == book.BookID).FirstOrDefault(a => a.UserID == currentUserId) != null)
            {
                var rates = db.Rates.FirstOrDefault(a => a.BookID == book.BookID) ?? null;
                ViewBag.IsUserReviewd = rates == null ? 0 : Convert.ToInt32(db.Rates.Where(a => a.BookID == book.BookID).FirstOrDefault(a => a.UserID == currentUserId).RateValue);
                return View(book);
            }

            ViewBag.IsUserReviewd = 0;
            return View(book);
        }

        // GET : The User Review for The Book
        [HttpGet]
        public ActionResult _UserReviewPost(int rate, int Id)
        {
            Rates rateValue = new Rates();
            var book = db.Books.Find(Id);

            switch (rate)
            {
                case 1:
                   rateValue.RateValue = RateValue.VeryWeak_1;
                    break;

                case 2:
                    rateValue.RateValue = RateValue.Weak_2;
                    break;

                case 3:
                    rateValue.RateValue = RateValue.Medium_3;
                    break;

                case 4:
                    rateValue.RateValue = RateValue.Good_4;
                    break;

                case 5:
                    rateValue.RateValue = RateValue.VeryGood_5;
                    break;


                default:
                    break;
            }

            rateValue.UserID = User.Identity.GetUserId();
            rateValue.BookID = book.BookID;
            db.Rates.Add(rateValue);

            db.SaveChanges();

            return RedirectToAction("Details",new { Id = book.BookID });
        }

        [HttpGet, ActionName("DeleteReview")]
        public ActionResult DeleteReviewToRedoIt(int BookId)
        {
            var currentUserId = User.Identity.GetUserId();
            Rates rates = db.Rates.Where(a => a.BookID == BookId).FirstOrDefault(a => a.UserID == currentUserId);
            db.Rates.Remove(rates);
            db.SaveChanges();
            return RedirectToAction("Details", new { Id = BookId });
        }



        // GET: Books/Details/5
        public ActionResult Details(int? Id)
        {
            var book = db.Books.Find(Id);
            var authors = db.Authors.Find(book.AuthorID);
            var categories = db.Category.Find(book.CategoryID);
            var commentsById = db.Comments.Where(a => (a.BookID == book.BookID)).ToList() == null ? null 
                             : db.Comments.Where(a => (a.BookID == book.BookID)).ToList();

            var model = new ViewModels.Book.Details(authors, categories, book, commentsById);

            TempData["LastBookId"] = book.BookID;

            return View(model);
        }

        public ActionResult DetailsBackToHome(int? Id)
        {
            var book = db.Books.Find(Id);
            var authors = db.Authors.Find(book.AuthorID);
            var categories = db.Category.Find(book.CategoryID);
            var commentsById = db.Comments.Where(a => (a.BookID == book.BookID)).ToList() == null ? null
                             : db.Comments.Where(a => (a.BookID == book.BookID)).ToList();

            var model = new ViewModels.Book.Details(authors, categories, book, commentsById);

            TempData["LastBookId"] = book.BookID;

            return View("DetailsBackToHome",model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveComment(ViewModels.Book.Details model)
        {
            var comment = new Comments();
            var ID = System.Web.HttpContext.Current.User.Identity.GetUserId();

            comment.Comment = model.Comment;
            db.Comments.Add(comment);
            db.SaveChanges();

            return View(model);
        }

        
        // GET: Books/Create
        public ActionResult Create()
        {
            var authors = db.Authors.ToList();
            var categories = db.Category.ToList();
            var model = new ViewModels.Book.Create(authors, categories);

            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.Book.Create model)
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

            model = new ViewModels.Book.Create(db.Authors.ToList(), db.Category.ToList());
            return View(model);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            var book = db.Books.Find(id);

            var authors = db.Authors.ToList();
            var categories = db.Category.ToList();
             
            var model = new ViewModels.Book.Edit( book, categories, authors);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.Book.Edit model)
        {

            var book = db.Books.Find(model.Id);

            if (ModelState.IsValid)
            {
                book.Modified = DateTime.Now.Ticks;
                book.BookName = model.Name;
                book.Description = model.Discription;
                book.AuthorID = model.SelectedAuthorId;
                book.CategoryID = model.SelectedCategoryId;

                if (model.Upload != null)
                {
                    book.Image = ImageArray.ToByteArray(model.Upload);
                }

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
            var model = new ViewModels.Book.Delete(books);
            return View(model);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ViewModels.Book.Delete model)
        {
            var book = db.Books.Find(model.Id);

            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

    }
}
