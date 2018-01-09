namespace SPCF.ViewModels.Book
{
    using DataBase.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Edit
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public HttpPostedFileBase Upload { get; set; }

        public string Discription { get; set; }
            public int SelectedAuthorId { get; set; }
            public int SelectedCategoryId { get; set; }

            public IEnumerable<ViewModels.Author.GAuthor> Authors { get; set; }
            public IEnumerable<ViewModels.Category.GCategory> Categoties { get; set; }



            public Edit()
            {
            }

            public Edit(Books book, IEnumerable<Category> categories, IEnumerable<Authors> authors)
            {
                Id = book.BookID;
                Name = book.BookName;
                Discription = book.Description;
                Image = book.Image == null ? null : Convert.ToBase64String(book.Image);

                SelectedAuthorId = book.AuthorID;
                SelectedCategoryId = book.CategoryID;

                Authors = authors.Select(a => new ViewModels.Author.GAuthor(a));
                Categoties = categories.Select(a => new ViewModels.Category.GCategory(a));
            }
        }
}

