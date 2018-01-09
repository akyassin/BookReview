using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPCF.ViewModels;
using DataBase.Entities;
using SPCF.Models;

namespace SPCF.ViewModels.Rating
{
    public class Index
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public RateValue Value { get; set; }
        public long? Created { get; set; }
        public long? Modified { get; set; }



        public Index()
        {

        }

        public Index(Rates rate)
        {
            var db = new SPContext();
            BookName = db.Books.Where(a => (a.BookID == rate.BookID)).FirstOrDefault().BookName;
            UserName = db.Users.Where(a => (a.Id == rate.UserID)).FirstOrDefault().UserName;
            Created = rate.Created;
            Modified = rate.Modified;
            Value = rate.RateValue;
            Id = rate.RateID;
        }
    }
}