using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondProjectCodeFirst.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }

        public AuthorViewModel()
        {
        }

        public AuthorViewModel(Authors author)
        {
            AuthorID = author.AuthorID;
            AuthorName = $"{author.FirstName} {author.LastName}";
        }
    }
}