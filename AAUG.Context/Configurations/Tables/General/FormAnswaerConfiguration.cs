using AAUG.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class FormAnswearConfiguration : IEntityTypeConfiguration<FormAnswear>
{
    public void Configure(EntityTypeBuilder<FormAnswear> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("FormAnswears", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        builder.Property(a => a.Answear).HasColumnName("Answear");
        builder.Property(a => a.FormQuestionId).HasColumnName("FormQuestionId");
        builder.Property(a => a.AnswearerUserId).HasColumnName("AnswearerUserId");
        #endregion

        #region Relations

        #endregion
    }
}
