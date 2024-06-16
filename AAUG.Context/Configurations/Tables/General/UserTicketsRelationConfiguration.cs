using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

public class UserTicketsRelationConfiguration
{
    internal class NewsConfiguration : IEntityTypeConfiguration<UserTicketsRelation>
    {
        public void Configure(EntityTypeBuilder<UserTicketsRelation> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion
            
            #region TableMapping
            builder.ToTable("UserTicketsRelations", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.TicketId).HasColumnName("TicketId");
            builder.Property(a => a.UserId).HasColumnName("UserId");
        
            
            #endregion

            #region Relations
            builder.HasOne(utr => utr.AaugUser)
                .WithMany(au => au.UserTicketsRelations)
                .HasForeignKey(utr => utr.UserId);

            builder.HasOne(utr => utr.Ticket)
                .WithMany(t => t.UserTicketsRelations)
                .HasForeignKey(utr => utr.TicketId);
            #endregion
        }
    }
}
