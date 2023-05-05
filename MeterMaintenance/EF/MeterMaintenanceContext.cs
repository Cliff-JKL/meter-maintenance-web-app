using MeterMaintenance.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterMaintenance.EF;

public class MeterMaintenanceContext : DbContext, IMeterMaintenanceContext
{
    public DbSet<Meter> Meter { get; set; } = null!;
    public DbSet<Apartment> Apartment { get; set; } = null!;

    public MeterMaintenanceContext(DbContextOptions<MeterMaintenanceContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();

            entity.Property(m => m.SerialNumber)
                .HasColumnName("serial_number")
                .IsRequired();

            entity.Property(m => m.LastReadingDate)
                .HasColumnName("last_reading_date")
                .IsRequired();

            entity.Property(m => m.NextReadingDate)
                .HasColumnName("next_reading_date")
                .IsRequired();

            entity.HasOne(m => m.Apartment)
                .WithOne(a => a.CurrentMeter)
                .HasForeignKey<Apartment>(a => a.CurrentMeterId);
            
            entity.HasOne(m => m.MeterReading)
                .WithOne(r => r.Meter)
                .HasForeignKey<MeterReading>(r => r.MeterId);
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).ValueGeneratedOnAdd();

            entity.Property(r => r.Value)
                .HasColumnName("value")
                .IsRequired();

            entity.Property(r => r.ReadingDate)
                .HasColumnName("reading_date")
                .IsRequired();
            
            entity.Property(r => r.MeterId).HasColumnName("meter_id");
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();

            entity.Property(a => a.Name)
                .HasColumnName("name")
                .IsRequired();

            entity.Property(a => a.CurrentMeterId).HasColumnName("current_meter_id");
        });

        modelBuilder.Entity<Meter>().HasData(
            new
            {
                Id = 1,
                SerialNumber = "abc123-2023",
                LastReadingDate = new DateTime(2023, 4, 10),
                NextReadingDate = new DateTime(2023, 5, 10),
            }
        );

        modelBuilder.Entity<MeterReading>().HasData(
            new
            {
                Id = 1,
                Value = 342.5,
                ReadingDate = new DateTime(2023, 4, 10),
                MeterId = 1,
            }
        );

        modelBuilder.Entity<Apartment>().HasData(
            new
            {
                Id = 1,
                Name = "Parkovy/2/15",
                CurrentMeterId = 1,
            },
            new
            {
                Id = 2,
                Name = "Lenina/22/4",
            }
        );
    }
}