using MeterMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MeterMaintenance.EF;

public interface IMeterMaintenanceContext
{
    ChangeTracker ChangeTracker { get; }
    DatabaseFacade Database { get; }
    
    public DbSet<Meter> Meter { get; set; }
    public DbSet<Apartment> Apartment { get; set; }
    public DbSet<MeterReading> MeterReading { get; set; }
    public DbSet<MeterReplacementHistory> MeterReplacementHistories { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}