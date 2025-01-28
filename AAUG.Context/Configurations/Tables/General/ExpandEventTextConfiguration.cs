using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class ExpandEventTextConfiguration : IEntityTypeConfiguration<ExpandEventText>
{
    public void Configure(EntityTypeBuilder<ExpandEventText> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("Expandeventtexts", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.EventId).HasColumnName("EventId");
        builder.Property(a => a.OrderBy).HasColumnName("OrderBy");
        builder.Property(a => a.Details).HasColumnName("Details");
        #endregion

        #region Relations
        #endregion
    }
}