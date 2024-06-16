using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;


internal class FormQuestionConfiguration : IEntityTypeConfiguration<FormQuestion>
{
    public void Configure(EntityTypeBuilder<FormQuestion> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("FormQuestions", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.CreatorUserId).HasColumnName("CreatorUserId");
        builder.Property(a => a.FormTopic).HasColumnName("FormTopic");
        builder.Property(a => a.Question).HasColumnName("Question");
        #endregion

        #region Relations

        #endregion
    }
}

