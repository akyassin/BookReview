namespace SPCF.Models.Interfaces
{
    using System.Data.Entity.ModelConfiguration;
    public interface IStoreable
    {
        long? Created { get; set; }
        long? Modified { get; set; }
        
    }

    public sealed partial class EfConfigureInterface
    {
        public static void Storeable<T>(EntityTypeConfiguration<T> conf)
            where T : class, IStoreable
        {
            conf.Property(a => a.Created).IsRequired();
            conf.Property(a => a.Modified).IsOptional();
        }

    }

}