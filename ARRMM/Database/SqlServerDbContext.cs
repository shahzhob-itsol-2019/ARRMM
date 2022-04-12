using ARRMM.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ARRMM.Database
{
    public class SqlServerDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IDbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public SqlServerDbContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .HasIndex(u => u.NicNumber)
                   .IsUnique();

            builder.Entity<Coordinate>()
                   .Property(a => a.Latitude).HasPrecision(9, 7);

            builder.Entity<Coordinate>()
                   .Property(a => a.Longitude).HasPrecision(9, 7);
        }

        public int SaveChanges(CancellationToken cancellationToken = default)
        {
            return base.SaveChanges();
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Coordinate> Coordinates { get; set; }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Mineral> Minerals { get; set; }
        public virtual DbSet<ApplicationMineral> ApplicationMinerals { get; set; }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Reconnaissance> Reconnaissances { get; set; }
        public virtual DbSet<Exploration> Explorations { get; set; }
        public virtual DbSet<MineralDepositRetention> MineralDepositRetentions { get; set; }
        public virtual DbSet<LargeMiningLease> LargeMiningLeases { get; set; }
        public virtual DbSet<Prospecting> Prospectings { get; set; }
        public virtual DbSet<SmallMiningLease> SmallMiningLeases { get; set; }
        public virtual DbSet<LandSurrenderAndTransfer> LandSurrendersAndTransfers { get; set; }
    }
}
