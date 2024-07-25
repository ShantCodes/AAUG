using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;


internal class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("MediaFiles", "Media");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Gid).HasColumnName("Gid");
        builder.Property(a => a.MediaFolderId).HasColumnName("MediaFolderId");
        builder.Property(a => a.Name).HasColumnName("Name");
        builder.Property(a => a.Extension).HasColumnName("Extension");
        builder.Property(a => a.Size).HasColumnName("Size");
        builder.Property(a => a.IsOmit).HasColumnName("IsOmit");
        builder.Property(a => a.Date).HasColumnName("Date");


        #endregion

        #region Relations
        builder
            .HasMany(m => m.AaugUserProfilePictureFiles)
            .WithOne(u => u.ProfilePictureFile)
            .HasForeignKey(u => u.ProfilePictureFileId);

        builder
            .HasMany(m => m.AaugUserNationalCardFiles)
            .WithOne(u => u.NationalCardFile)
            .HasForeignKey(u => u.NationalCardFileId);

        builder
            .HasMany(m => m.AaugUserUniversityCardFiles)
            .WithOne(u => u.UniversityCardFile)
            .HasForeignKey(u => u.UniversityCardFileId);

        #endregion
    }
}

