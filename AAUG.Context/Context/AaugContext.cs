using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
namespace AAUG.Context.Context
{
    public class AaugContext : DbContext
    {

        public AaugContext(DbContextOptions<AaugContext> options)
            : base(options)
        {

        }

        #region DbSets
        public virtual DbSet<AaugUser> AaugUsers { get; set; }

        public virtual DbSet<Camp> Camps { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventLike> EventLikes { get; set; }

        public virtual DbSet<MediaFile> MediaFiles { get; set; }

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<UserMajor> UserMajors { get; set; }

        public virtual DbSet<UserTalent> UserTalents { get; set; }

        #endregion

        #region Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.AaugUserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.CampConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.EventConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.EventLikeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.NewsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.UserMajorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.UserTalentConfiguration());
            #endregion
        }

        #endregion

    }
}
