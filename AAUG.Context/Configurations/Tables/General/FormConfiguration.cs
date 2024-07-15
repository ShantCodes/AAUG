using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General;

internal class FormConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        #region Primary Key
        builder.HasKey(x => x.Id);
        #endregion

        #region TableMapping
        builder.ToTable("Forms", "General");
        #endregion

        #region Column Mappings
        builder.Property(a => a.Id).HasColumnName("Id");
        //builder.Property(a => a.FormQuestions).HasColumnName("FormQuestions");
        builder.Property(a => a.CreatorUserId).HasColumnName("CreatorUserId");
        builder.Property(a => a.FormTitle).HasColumnName("FormTitle");
        #endregion

        #region Relations

        #endregion
    }
}