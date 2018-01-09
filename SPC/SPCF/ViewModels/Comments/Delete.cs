namespace SPCF.ViewModels.Comments
{
    using DataBase.Entities;
    using SPCF.Models;



    public class Delete
    {

        private SPContext db = new SPContext();


        public int Id { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public string Comment { get; set; }

        public Delete()
        {
        }

        public Delete(Comments comment)
        {
            var book = db.Books.Find(comment.BookID);
            var user = db.UserProfiles.Find(comment.UserID);

            Id = comment.CommentID;
            UserName = user.fullName;
            BookName = book.BookName;
            Comment = comment.Comment;

        }

    }
    
}