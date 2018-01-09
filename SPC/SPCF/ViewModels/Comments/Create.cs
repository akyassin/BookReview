namespace SPCF.ViewModels.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using DataBase.Entities;
    using SPCF.Models;

    public class Create
    {
        public string Id { get; set; }
        public string Comment { get; set; }

        public string selectedUserId { get; set; }
        public int selecedBookId { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ViewModels.Book.GBook> Books { get; set; }



        public Create()
        {
        }

        public Create(IEnumerable<ApplicationUser> users,IEnumerable<Books> books)
        {
            Users = users.Select(a => new UserViewModel(a));
            Books = books.Select(a => new Book.GBook(a));
        }





    }
}