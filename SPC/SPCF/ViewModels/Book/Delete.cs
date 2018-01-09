using DataBase.Entities;
using System;

namespace SPCF.ViewModels.Book
{
        public class Delete
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }

            public Delete()
            {
            }

            public Delete(Books book)
            {
                Id = book.BookID;
                Name = book.BookName;
                Image = book.Image == null ? null : Convert.ToBase64String(book.Image);
            }
        }
}