using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public virtual DbSet<FormAnswear> FormAnswears { get; set; }

        public virtual DbSet<FormQuestion> FormQuestions { get; set; }

        public virtual DbSet<Form> Forms { get; set; }

        public virtual DbSet<MediaDrive> MediaDrives { get; set; }

        public virtual DbSet<MediaFolder> MediaFolders { get; set; }

        public virtual DbSet<Suggestion> Suggestions { get; set; }

        public virtual DbSet<SuggestionVote> SuggestionVotes { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public DbSet<UserTicketsRelation> UserTicketsRelations { get; set; }

        public virtual DbSet<SlideShow> SlideShows { get; set; }

        public virtual DbSet<SlideShowTitle> SlideShowsTitles { get; set; }




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
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.SuggestionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.SuggestionVoteConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.TicketConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.FormAnswearConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.FormQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.MediaFileConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.MediaFolderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.MediaDriveConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.UserMajorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.UserTalentConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.FormConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.SlideShowConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Tables.General.SlideShowTitleConfiguration());
            #endregion
        }

        #endregion

    }

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
