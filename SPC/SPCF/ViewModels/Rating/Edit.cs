using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPCF.ViewModels;
using DataBase.Entities;
using SPCF.Models;

namespace SPCF.ViewModels.Rating
{
    public class Edit
    {
        public int Id { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public string SelectedUserId { get; set; }
        public IEnumerable<Book.GBook> Books { get; set; }
        public int SelectedBookId { get; set; }
        public RateValue Value { get; set; }



        public Edit()
        {

        }

        public Edit(IEnumerable<Books> books, IEnumerable<ApplicationUser> users, Rates rate)
        {
            Books = books.Select(a => new Book.GBook(a));
            Users = users.Select(a => new UserViewModel(a));
            SelectedBookId = rate.BookID;
            SelectedUserId = rate.UserID;
        }
    }
}