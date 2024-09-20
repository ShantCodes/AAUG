using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class SlideShowConfiguration : IEntityTypeConfiguration<SlideShow>
{
    public void Configure(EntityTypeBuilder<SlideShow> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("SlideShows", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.MediaFileId).HasColumnName("MediaFileId");
        builder.Property(a => a.Description).HasColumnName("Description");
        builder.Property(a => a.TitleId).HasColumnName("TitleId");
        builder.Property(a => a.IsActive).HasColumnName("IsActive");


        #endregion

        #region Relations
        // builder.HasOne(a => a.Title)
        // .WithMany(a => a.SlideShows)
        // .HasForeignKey(a => a.TitleId);

        builder.HasOne(a => a.MediaFile)
        .WithMany(a => a.SlideShows)
        .HasForeignKey(a => a.MediaFileId);
        #endregion
    }
}