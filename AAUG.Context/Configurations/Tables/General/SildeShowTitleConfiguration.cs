using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class SlideShowTitleConfiguration : IEntityTypeConfiguration<SlideShowTitle>
{
    public void Configure(EntityTypeBuilder<SlideShowTitle> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("SlideShowTitles", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Description).HasColumnName("Description");


        #endregion

        #region Relations
        // builder.HasMany(a => a.SlideShows)
        // .WithOne(a => a.Title)
        // .HasForeignKey(a => a.TitleId);
        #endregion
    }
}