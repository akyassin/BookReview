namespace SPCF.ViewModels.Author
{
    using DataBase.Entities;
    using System;

    public class Delete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public Delete()
        {
        }
        public Delete(Authors author)
        {
            Id = author.AuthorID;
            Name = author.fullName;
            Image = author.Image == null ? null : Convert.ToBase64String(author.Image);

        }
    }
}