using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion
            
            #region TableMapping
            builder.ToTable("Events", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.EventTitle).HasColumnName("EventTitle");
            builder.Property(a => a.EventDetails).HasColumnName("EventDetails");
            builder.Property(a => a.Presentator).HasColumnName("Presentator");
            builder.Property(a => a.PresentatorUserId).HasColumnName("PresentatorUserId");
            builder.Property(a => a.ThumbNailFileId).HasColumnName("ThumbNailFileId");
            builder.Property(a => a.IsApproved).HasColumnName("IsApproved");
            builder.Property(a => a.HasHappened).HasColumnName("HasHappened");
            builder.Property(a => a.EventDate).HasColumnName("EventDate");
            builder.Property(a => a.LikeCount).HasColumnName("LikeCount");
            #endregion

            #region Relations
            builder.HasMany(a=> a.EventLikes)
            .WithOne(a=> a.Event)
            .HasForeignKey(a => a.EventId);
            #endregion
        }
    }
}
