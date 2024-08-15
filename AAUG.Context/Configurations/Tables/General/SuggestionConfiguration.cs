using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;


internal class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("Suggestions", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Title).HasColumnName("Title");
        builder.Property(a => a.Description).HasColumnName("Description");
        builder.Property(a => a.Date).HasColumnName("Date");
        builder.Property(a => a.IsApproved).HasColumnName("IsApproved");
        builder.Property(a => a.UserId).HasColumnName("UserId");
        builder.Property(a => a.Expired).HasColumnName("Expired");


        #endregion

        #region Relations
        builder.HasMany(a => a.SuggestionVotes)
        .WithOne(a => a.Suggestion)
        .HasForeignKey(a => a.SuggestionId);

        // builder.HasOne(a => a.AaugUser)
        // .WithMany(a => a.Suggestions)
        // .HasForeignKey(a => a.UserId);
        #endregion
    }
}

