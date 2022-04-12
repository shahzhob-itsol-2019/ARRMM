using ARRMM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARRMM.Database
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        int SaveChanges(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<Country> Countries { get; set; }
        DbSet<Status> Statuses { get; set; }

        DbSet<Location> Locations { get; set; }
        DbSet<Coordinate> Coordinates { get; set; }

        DbSet<Person> Persons { get; set; }
        DbSet<Company> Companies { get; set; }

        DbSet<Mineral> Minerals { get; set; }
        DbSet<ApplicationMineral> ApplicationMinerals { get; set; }

        DbSet<Application> Applications { get; set; }
        DbSet<Reconnaissance> Reconnaissances { get; set; }
        DbSet<Exploration> Explorations { get; set; }
        DbSet<MineralDepositRetention> MineralDepositRetentions { get; set; }
        DbSet<LargeMiningLease> LargeMiningLeases { get; set; }
        DbSet<Prospecting> Prospectings { get; set; }
        DbSet<SmallMiningLease> SmallMiningLeases { get; set; }
        DbSet<LandSurrenderAndTransfer> LandSurrendersAndTransfers { get; set; }
    }
}
