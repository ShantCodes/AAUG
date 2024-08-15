using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class SuggestionVoteConfiguration : IEntityTypeConfiguration<SuggestionVote>
{
    public void Configure(EntityTypeBuilder<SuggestionVote> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("SuggestionVotes", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.SuggestionId).HasColumnName("SuggestionId");
        builder.Property(a => a.UserId).HasColumnName("UserID");
        builder.Property(a => a.Vote).HasColumnName("Vote");


        #endregion

        #region Relations
        // builder.HasOne(a => a.AaugUser)
        // .WithMany(a => a.SuggestionVotes)
        // .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Suggestion)
        .WithMany(a => a.SuggestionVotes)
        .HasForeignKey(a => a.SuggestionId);

        #endregion
    }
}