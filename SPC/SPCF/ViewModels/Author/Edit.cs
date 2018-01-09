namespace SPCF.ViewModels.Author
{
    using DataBase.Entities;
    using System;
    using System.Web;

    public class Edit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public HttpPostedFileBase Upload { get; set; }

        public Edit()
        {
        }

        public Edit(Authors author)
        {
            Id = author.AuthorID;
            FirstName = author.FirstName;
            LastName = author.LastName;
            Description = author.Description;
            Image = author.Image == null ? null : author.Image;
        }
    }
}