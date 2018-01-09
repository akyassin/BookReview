namespace SPCF.ViewModels.Category
{
    using DataBase.Entities;
    public class GCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GCategory()
        {
        }
        public GCategory(Category category)
        {
            Id = category.CategoryID;
            Name = category.CategoryName;
        }
    }
}