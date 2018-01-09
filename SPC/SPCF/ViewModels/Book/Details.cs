namespace SPCF.ViewModels.Book
{
    using DataBase.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Details
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Discription { get; set; }
        public string  Comment { get; set; }
        public IEnumerable<ViewModels.Comments.GComment> Comments { get; set; }
        public ViewModels.Author.GAuthor Author { get; set; }
        public ViewModels.Category.GCategory Category { get; set; }


        public Details()
        {
        }

        public Details( Authors authors, Category categories, Books book, IEnumerable<Comments> comments)
        {
            Id = book.BookID; 
            Author = new ViewModels.Author.GAuthor(authors);
            Category = new ViewModels.Category.GCategory(categories);
            Name = book.BookName;
            Discription = book.Description;
            Image = book.Image == null ? null : Convert.ToBase64String(book.Image);
            Comments = comments.Select(c => new ViewModels.Comments.GComment(c));

        }


    }
}






   

