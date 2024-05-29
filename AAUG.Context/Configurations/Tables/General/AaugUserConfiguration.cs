using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class AaugUserConfiguration : IEntityTypeConfiguration<AaugUser>
    {
        public void Configure(EntityTypeBuilder<AaugUser> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("AaugUsers", "General");
            #endregion

            #region Column Mappings
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.LastName).HasColumnName("Name");
            builder.Property(x => x.NameArmenian).HasColumnName("Name");
            builder.Property(x => x.LastNameArmenian).HasColumnName("Name");
            builder.Property(x => x.RoleId).HasColumnName("Name");
            builder.Property(x => x.MajorsId).HasColumnName("Name");
            builder.Property(x => x.TalentsId).HasColumnName("Name");
            builder.Property(x => x.ProfilePictureFileId).HasColumnName("Name");
            builder.Property(x => x.NationalCardFileId).HasColumnName("Name");
            builder.Property(x => x.Email).HasColumnName("Name");
            builder.Property(x => x.CanGetNotfiedByMail).HasColumnName("Name");
            builder.Property(x => x.IsApproved).HasColumnName("Name");

            #endregion

            #region Relations

            builder.HasMany(a => a.Events)
                .WithOne(a => a.PresentatorUser)
                .HasForeignKey(a => a.PresentatorUserId);

            builder.HasMany(a => a.UserMajors)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.UserTalents)
            .WithOne(a=>a.User)
            .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.Camps)
            .WithOne(a => a.RepresentatorUser)
            .HasForeignKey(a => a.RepresentatorUserId);

            // builder.HasMany(a => a.EventLikes)
            // .WithMany(a => a.User)
            // .HasForeignKey();
            #endregion
        }
    }
}
