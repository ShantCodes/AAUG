using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion
            
            #region TableMapping
            builder.ToTable("News", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.NewsTitle).HasColumnName("NewsTitle");
            builder.Property(a => a.NewsDetails).HasColumnName("NewsDetails");
        
            
            #endregion

            #region Relations
            
            #endregion
        }
    }
}
