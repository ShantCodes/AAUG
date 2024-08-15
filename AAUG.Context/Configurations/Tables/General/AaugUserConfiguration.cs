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
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.NameArmenian).HasColumnName("NameArmenian");
            builder.Property(x => x.LastNameArmenian).HasColumnName("LastNameArmenian");
            builder.Property(x => x.MajorsId).HasColumnName("MajorsId");
            builder.Property(x => x.TalentsId).HasColumnName("TalentsId");
            builder.Property(x => x.ProfilePictureFileId).HasColumnName("ProfilePictureFileId");
            builder.Property(x => x.NationalCardFileId).HasColumnName("NationalCardFileId");
            builder.Property(x => x.ReceiptFileId).HasColumnName("ReceiptFileId");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.CanGetNotfiedByMail).HasColumnName("CanGetNotfiedByMail");
            builder.Property(x => x.IsApproved).HasColumnName("IsApproved");
            builder.Property(x => x.SubscribeDate).HasColumnName("SubscribeDate");

            #endregion

            #region Relations

            builder.HasMany(a => a.Events)
                .WithOne(a => a.PresentatorUser)
                .HasForeignKey(a => a.PresentatorUserId);

            // builder.HasMany(a => a.UserMajors)
            // .WithOne(a => a.User)
            // .HasForeignKey(a => a.UserId);

            // builder.HasMany(a => a.UserTalents)
            // .WithOne(a => a.User)
            // .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.Camps)
            .WithOne(a => a.RepresentatorUser)
            .HasForeignKey(a => a.RepresentatorUserId);

            // builder.HasMany(a => a.UserTicketsRelations)
            //     .WithOne(a => a.AaugUser)
            //     .HasForeignKey(a => a.UserId);

            // builder.HasOne(u => u.ProfilePictureFile)
            // .WithMany(m => m.AaugUserProfilePictureFiles)
            // .HasForeignKey(u => u.ProfilePictureFileId)
            // .IsRequired(false);

            // builder.HasOne(u => u.NationalCardFile)
            //     .WithMany(m => m.AaugUserNationalCardFiles)
            //     .HasForeignKey(u => u.NationalCardFileId)
            //     .IsRequired();

            // builder.HasOne(u => u.UniversityCardFile)
            //     .WithMany(m => m.AaugUserUniversityCardFiles)
            //     .HasForeignKey(u => u.UniversityCardFileId)
            //     .IsRequired();

            // builder.HasOne(u => u.ReceiptFile)
            //     .WithMany(m => m.AaugUserReceiptFiles)
            //     .HasForeignKey(u => u.ReceiptFileId)
            //     .IsRequired();


            #endregion
        }
    }
}
