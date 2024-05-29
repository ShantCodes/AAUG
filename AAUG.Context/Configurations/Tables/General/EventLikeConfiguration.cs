using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class EventLikeConfiguration : IEntityTypeConfiguration<EventLike>
    {
        public void Configure(EntityTypeBuilder<EventLike> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("EventLikes", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.EventId).HasColumnName("EventId");
            builder.Property(a => a.UserId).HasColumnName("UserId");
            #endregion

            #region Relations
            builder.HasOne(el => el.Event)
                .WithMany(e => e.EventLikes)
                .HasForeignKey(el => el.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(el => el.User)
                .WithMany(u => u.EventLikes)
                .HasForeignKey(el => el.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
