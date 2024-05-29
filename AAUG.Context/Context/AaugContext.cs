using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAUG.Context.Context
{
    internal class AaugContext : DbContext
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
            #endregion
        }

        #endregion

    }
}
