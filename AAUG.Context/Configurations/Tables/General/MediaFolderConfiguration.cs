using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class MediaFolderConfiguration : IEntityTypeConfiguration<MediaFolder>
{
    public void Configure(EntityTypeBuilder<MediaFolder> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("MediaFolders", "Media");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.MediaDriveId).HasColumnName("MediaDriveId");
        builder.Property(a => a.Name).HasColumnName("Name");
        builder.Property(a => a.ParentId).HasColumnName("ParentId");
        builder.Property(a => a.MediaPathTypeId).HasColumnName("MediaPathTypeId");

        #endregion

        #region Relations

        #endregion
    }
}

