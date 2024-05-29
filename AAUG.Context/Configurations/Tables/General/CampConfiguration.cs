using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class CampConfiguration : IEntityTypeConfiguration<Camp>
    {
        public void Configure(EntityTypeBuilder<Camp> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("Camps", "General");
            #endregion

            #region Column Mappings
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CampingTitle).HasColumnName("CampingTitle").IsRequired();
            builder.Property(x => x.CampDetails).HasColumnName("CampDetails");
            builder.Property(x => x.RepresentatorUserId).HasColumnName("RepresentatorUserId").IsRequired();
            builder.Property(x => x.StartingDate).HasColumnName("StartingDate").IsRequired();
            builder.Property(x => x.EndDate).HasColumnName("EndDate").IsRequired();
            builder.Property(x => x.HasHappened).HasColumnName("HasHappened").IsRequired();
            builder.Property(x => x.UserMaxCapacity).HasColumnName("UserMaxCapacity").IsRequired();
            #endregion

            #region Relations
            builder.HasOne(c => c.RepresentatorUser)
                   .WithMany(u => u.Camps)
                   .HasForeignKey(c => c.RepresentatorUserId);
            #endregion
        }
    }
}
