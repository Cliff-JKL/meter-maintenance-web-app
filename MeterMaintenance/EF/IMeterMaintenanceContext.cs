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

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}