using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPCF.ViewModels;
using DataBase.Entities;
using SPCF.Models;

namespace SPCF.ViewModels.Rating
{
    public class Create
    {
        public int Id { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public string SelectedUserId { get; set; }
        public IEnumerable<Book.GBook> Books { get; set; }
        public int SelectedBookId { get; set; }
        public RateValue Value { get; set; }

        public Create()
        {

        }

        public Create(IEnumerable<Books> books, IEnumerable<ApplicationUser> users)
        {
            Books = books.Select(a => new Book.GBook(a));
            Users = users.Select(a => new UserViewModel(a));
        }
    }
}