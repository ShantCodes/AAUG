using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AAUG.Context.Configurations.Tables.General
{
    internal class PushSubscriptionConfiguration : IEntityTypeConfiguration<PushSubscription>
    {
        public void Configure(EntityTypeBuilder<PushSubscription> builder)
        {
            #region Primary Key
            builder.HasKey(x => x.Id);
            #endregion

            #region TableMapping
            builder.ToTable("PushSubscriptions", "General");
            #endregion

            #region Column Mappings
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.EndPoint).HasColumnName("EndPoint");
            builder.Property(x => x.P256dhKey).HasColumnName("P256dhKey");
            builder.Property(x => x.AuthKey).HasColumnName("AuthKey");
            #endregion

            #region Relations

            #endregion
        }
    }
}
