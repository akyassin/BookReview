namespace SPCF.ViewModels.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using DataBase.Entities;
    using SPCF.Models;

    public class Edit
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int selectedBookId { get; set; }
        public string selectedUserId { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ViewModels.Book.GBook> Books { get; set; }

        public Edit()
        {
        }

        public Edit(IEnumerable<ApplicationUser> users ,IEnumerable<Books> books, DataBase.Entities.Comments comment)
        {
            Users = users.Select(a => new UserViewModel(a));
            Books = books.Select(a => new Book.GBook(a));
            selectedBookId = comment.BookID;
            selectedUserId = comment.UserID;
            Comment = comment.Comment;
        }


    }
}