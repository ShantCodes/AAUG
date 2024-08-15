using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class UserTalentConfiguration : IEntityTypeConfiguration<UserTalent>
    {
        public void Configure(EntityTypeBuilder<UserTalent> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion
            
            #region TableMapping
            builder.ToTable("UserTalent", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.Talent).HasColumnName("Talent");
            builder.Property(a => a.UserId).HasColumnName("UserId");

            #endregion

            #region Relations
            // builder.HasOne(um => um.User)
            //                 .WithMany(u => u.UserTalents)
            //                 .HasForeignKey(um => um.UserId)
            //                 .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}
