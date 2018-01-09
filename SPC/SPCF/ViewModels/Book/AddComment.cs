namespace SPCF.ViewModels.Book
{

    using DataBase.Entities;
    using Microsoft.AspNet.Identity;
    using SPCF.Models;
    using System.ComponentModel.DataAnnotations;

    public class AddComment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You can write max 500 letters...from required---from ModelView")]
        [MaxLength(100)]
        public string Comment { get; set; }
        public long? Created { get; set; }
        public long? Modified { get; set; }
        public int BookID { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }



        public AddComment()
        {
        }

        public AddComment(Books book, ApplicationUser user)
        {
            BookID = book.BookID;
            UserName = user.UserName;
            UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }


    }
}