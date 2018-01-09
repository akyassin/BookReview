using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondProjectCodeFirst.ViewModels
{
    public class BookViewModel
    {
        public class Create
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public HttpPostedFileBase Upload { get; set; }

            public int SelectedAuthorId { get; set; }
            public int SelectedCategoryId { get; set; }
            public IEnumerable<AuthorViewModel> Authors { get; set; }
            public IEnumerable<CategoryViewModel> Categories { get; set; }

            public Create() 
            {
            }

            public Create(IEnumerable<Authors> authors, IEnumerable<Category> categories)
            {
                Authors = authors.Select(a => new AuthorViewModel(a));

                //The upper line doing the same function of what we did down:
                /*
                var authorTempList = new List<AuthorViewModel>();
                foreach (var item in authors)
                {
                    var authorViewModel = new AuthorViewModel(item);
                    authorTempList.Add(authorViewModel);
                }
                Authors = authorTempList;
                */

                Categories = categories.Select(a => new CategoryViewModel(a));
            }
        }
        public class Details
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Discription { get; set; }
            public string Image { get; set; }
            public AuthorViewModel Author { get; set; }
            public CategoryViewModel Category { get; set; }

            public Details()
            {
            }

            public Details( Authors authors, Category categories, Books book)
            {
                Id = book.BookID; 
                Author = new AuthorViewModel(authors);
                Category = new CategoryViewModel(categories);
                Name = book.BookName;
                Discription = book.Description;
                Image = book.Image == null ? null : Convert.ToBase64String(book.Image);
            }
        }

        public class Edit
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Discription { get; set; }
            public string Image { get; set; }

            public int SelectedAuthorId { get; set; }
            public int SelectedCategoryId { get; set; }

            public IEnumerable<AuthorViewModel> Authors { get; set; }
            public IEnumerable<CategoryViewModel> Categoties { get; set; }

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

                Authors = authors.Select(a => new AuthorViewModel(a));
                Categoties = categories.Select(a => new CategoryViewModel(a));
            }
        }

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

}