using Microsoft.EntityFrameworkCore;
using InfluencePWA.Data.Models;

namespace InfluencePWA.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        #endregion Constructor

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map Entity names to DB Table names
            modelBuilder.Entity<Principle>().ToTable("Principles");
            modelBuilder.Entity<PrincipleType>().ToTable("PrincipleTypes");
        }
        #endregion Methods

        #region Properties
        public DbSet<Principle> Principles { get; set; }
        public DbSet<PrincipleType> PrincipleTypes { get; set; }
        #endregion Properties
    }
}
