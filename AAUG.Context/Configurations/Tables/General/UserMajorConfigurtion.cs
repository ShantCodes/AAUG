using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class UserMajorConfiguration : IEntityTypeConfiguration<UserMajor>
    {
        public void Configure(EntityTypeBuilder<UserMajor> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("UserMajors", "General");
            #endregion

            #region Column Mappings
            builder.Property(a => a.Id).HasColumnName("Id");
            builder.Property(a => a.UserId).HasColumnName("UserId");
            builder.Property(a => a.Major).HasColumnName("Major");
            builder.Property(a => a.HasGraduated).HasColumnName("HasGraduated");
            builder.Property(a => a.EntryDate).HasColumnName("EntryDate");
            builder.Property(a => a.GraduationDate).HasColumnName("GraduationDate");
            #endregion

            #region Relations
            // builder.HasOne(um => um.User)
            //                 .WithMany(u => u.UserMajors)
            //                 .HasForeignKey(um => um.UserId)
            //                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
