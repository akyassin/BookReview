using System;
using System.Net;
using SPCF.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DataBase.Entities;
using System.Collections.Generic;

namespace SPCF.Controllers
{

    public class RatesController : Controller
    {
        private SPContext db = new SPContext();

        // GET: Rates
        public ActionResult Index()
        {
            IEnumerable<Rates> rates = db.Rates.ToList();
            var model = rates.Select(a => new ViewModels.Rating.Index(a));
            return View(model);
        }

        [HttpGet]
        public ActionResult _BookReview(int Id)
        {
            var rates = db.Rates.Find(Id);
            var model = new ViewModels.Rating.Index(rates);
            return View(model);
        }

        [HttpPost]
        public ActionResult _BookReview()
        {
            return View();
        }

        // GET: Rates/Create
        public ActionResult Create()
        {
            var Books = db.Books.ToList();
            var Users = db.Users.ToList();

            var model = new ViewModels.Rating.Create(Books, Users);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.Rating.Create model)
        {
            if (ModelState.IsValid)
            {
                Rates ratevalue = new Rates();
                ratevalue.Created = DateTime.Now.Ticks;
                ratevalue.RateValue = model.Value;
                ratevalue.UserID = model.SelectedUserId;
                ratevalue.BookID = model.SelectedBookId;

                db.Rates.Add(ratevalue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Rates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rates rates = db.Rates.Find(id);

            if (rates == null)
            {
                return HttpNotFound();
            }
            var books = db.Books.ToList();
            var users = db.Users.ToList();
            var model = new ViewModels.Rating.Edit(books, users, rates);
            model.Value = rates.RateValue;
            model.Id = rates.RateID;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.Rating.Edit model)
        {
            if (ModelState.IsValid)
            {
                var rate = db.Rates.Find(model.Id);
                rate.Modified = DateTime.UtcNow.Ticks;
                rate.UserID = model.SelectedUserId;
                rate.BookID = model.SelectedBookId;
                rate.RateValue = model.Value;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        // POST: Rates/Delete/5
        [HttpGet, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rates rates = db.Rates.Find(id);
            db.Rates.Remove(rates);
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
