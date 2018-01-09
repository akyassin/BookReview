namespace SPCF.ViewModels.Author
{
    using DataBase.Entities;
    public class GAuthor
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }

        public GAuthor()
        {
        }

        public GAuthor(Authors author)
        {
            AuthorID = author.AuthorID;
            AuthorName = $"{author.FirstName} {author.LastName}";
        }
    }
}