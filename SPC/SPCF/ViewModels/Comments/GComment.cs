namespace SPCF.ViewModels.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using DataBase.Entities;
    using System;

    public class GComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public long? Created { get; set; }
        public long? Modified { get; set; }
        public string UserName { get; set; }



        public GComment()
        {
        }

        public GComment(Comments comment)
        {
            Comment = comment.Comment;
            Created = comment.Created;
            Modified = comment.Modified;

            var db = new Models.SPContext();
            var user = db.Users.Find(comment.UserID);
            UserName = user.UserName;

        }


    }
}