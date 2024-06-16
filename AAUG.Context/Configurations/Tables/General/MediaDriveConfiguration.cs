using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class MediaDriveConfiguration : IEntityTypeConfiguration<MediaDrive>
{
    public void Configure(EntityTypeBuilder<MediaDrive> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("MediaDrives", "Media");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Name).HasColumnName("Name");
        builder.Property(a => a.VirtualName).HasColumnName("VirtualName");
        #endregion

        #region Relations

        #endregion
    }
}
