namespace SPCF.ViewModels.Book
{
    using DataBase.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class Create
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Upload { get; set; }

        public int SelectedAuthorId { get; set; }
        public int SelectedCategoryId { get; set; }
        public IEnumerable<ViewModels.Author.GAuthor> Authors { get; set; }
        public IEnumerable<ViewModels.Category.GCategory> Categories { get; set; }

        public Create() 
        {
        }

        public Create(IEnumerable<Authors> authors, IEnumerable<Category> categories)   
        {
            Authors = authors.Select(a => new ViewModels.Author.GAuthor(a));
            Categories = categories.Select(a => new ViewModels.Category.GCategory(a));
            }
    }
}

