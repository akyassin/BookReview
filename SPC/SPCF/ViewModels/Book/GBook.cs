using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBase.Entities;

namespace SPCF.ViewModels.Book
{
    public class GBook
    {
        public int Id { get; set; }
        public string  BookName { get; set; }


        public GBook()
        {
        }

        public GBook(Books book)
        {
            Id = book.BookID;
            BookName = book.BookName;
        }
    }
}