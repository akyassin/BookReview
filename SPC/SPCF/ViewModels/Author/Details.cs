namespace SPCF.ViewModels.Author
{
    using DataBase.Entities;
    using System;

    public class Details
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }
        public string Image { get; set; }

        public Details()
        {
        }

        public Details(Authors author)
        {
            Id = author.AuthorID;
            Name = author.fullName;
            Describtion = author.Description;
            Image = author.Image == null ? null : Convert.ToBase64String(author.Image);
        }
    }
}