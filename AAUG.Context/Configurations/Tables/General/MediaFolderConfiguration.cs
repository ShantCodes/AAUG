using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context;

public class MediaFolderConfiguration
{
    internal class NewsConfiguration : IEntityTypeConfiguration<MediaFolder>
    {
        public void Configure(EntityTypeBuilder<MediaFolder> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("MediaFolders", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.MediaDriveId).HasColumnName("MediaDriveId");
            builder.Property(a => a.Name).HasColumnName("Name");
            builder.Property(a => a.ParentId).HasColumnName("ParentId");
            builder.Property(a => a.VirtualName).HasColumnName("VirtualName");


            #endregion

            #region Relations

            #endregion
        }
    }
}
