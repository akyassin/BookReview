using DataBase.Entities;
using SPCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace SPCF.Controllers
{

    public class HomeController : Controller
    {
        private SPContext db = new SPContext();


        public ActionResult Index()
        {
            var books = (db.Books.Include(b => b.Authors).Include(b => b.Category)).ToList();

            Random random = new Random();

            var randbooks = new List<Books>();

            while (randbooks.Count != 7)
            {
                var book = books[random.Next(books.Count)];
                while (!randbooks.Contains(book))
                {
                    randbooks.Add(book);
                }
            }

            return View(randbooks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}