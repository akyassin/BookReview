namespace SPCF.ViewModels.FavouriteBook
{
    using System.Linq;
    using SPCF.Models;
    using SPCF.ViewModels;
    using DataBase.Entities;
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FavouriteGeneral
    {
        public int Id { get; set; }

        public IEnumerable<Book.GBook> BooksList { get; set; }
        public IEnumerable<UserViewModel> UsersList { get; set; }

        public int selectedBookId { get; set; }
        public string selectedUserId { get; set; }

        public string BookName { get; set; }
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Created { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Modified { get; set; }


        public FavouriteGeneral()
        {
        }

        public FavouriteGeneral(IEnumerable<Books> books, IEnumerable<ApplicationUser> Users, FavouriteBooks favbook)
        {
            Id = favbook.FavouriteID;
            BooksList = books.Select(a => new Book.GBook(a));
            UsersList = Users.Select(a => new UserViewModel(a));
            selectedBookId = favbook.BookID;
            selectedUserId = favbook.UserID;

        }

        public FavouriteGeneral(FavouriteBooks favbook)
        {
            Id = favbook.FavouriteID;
            selectedBookId = favbook.BookID;
            selectedUserId = favbook.UserID;
            Created = new DateTime(favbook.Created??0);
            Modified = new DateTime(favbook.Modified ?? 0);

            using (var db = new SPContext())
            {
                UserProfiles user = db.UserProfiles.Find(selectedUserId);
                Books book = db.Books.Find(selectedBookId);
                UserName = user.fullName;
                BookName = book.BookName;
            }

        }



    }
}