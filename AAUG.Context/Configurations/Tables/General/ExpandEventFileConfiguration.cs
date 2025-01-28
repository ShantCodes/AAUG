using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class ExpandEventFileConfiguration : IEntityTypeConfiguration<ExpandEventFile>
{
    public void Configure(EntityTypeBuilder<ExpandEventFile> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("ExpandEventFiles", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.ExpandEventTextId).HasColumnName("EventId");
        builder.Property(a => a.MediaFileId).HasColumnName("OrderBy");
        #endregion

        #region Relations
        #endregion
    }
}