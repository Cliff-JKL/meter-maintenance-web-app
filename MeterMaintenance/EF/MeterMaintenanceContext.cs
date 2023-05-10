using MeterMaintenance.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterMaintenance.EF;

public class MeterMaintenanceContext : DbContext, IMeterMaintenanceContext
{
    public DbSet<Meter> Meter { get; set; } = null!;
    public DbSet<Apartment> Apartment { get; set; } = null!;
    public DbSet<MeterReading> MeterReading { get; set; } = null!;
    public DbSet<MeterReplacementHistory> MeterReplacementHistories { get; set; } = null!;

    public MeterMaintenanceContext(DbContextOptions<MeterMaintenanceContext> options) : base(options)
    {
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
                .HasColumnName("last_reading_date");

            entity.Property(m => m.NextReadingDate)
                .HasColumnName("next_reading_date");

            entity.HasOne(m => m.Apartment)
                .WithOne(a => a.CurrentMeter)
                .HasForeignKey<Apartment>(a => a.CurrentMeterId);
            
            entity.HasOne(m => m.MeterReading)
                .WithOne(r => r.Meter)
                .HasForeignKey<MeterReading>(r => r.MeterId);
                
            entity.HasOne(m => m.MeterReplacementHistory)
                .WithOne(r => r.NewMeter)
                .HasForeignKey<MeterReplacementHistory>(r => r.NewMeterId);
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
            
            entity.Property(r => r.MeterId)
                .HasColumnName("meter_id")
                .IsRequired();
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();

            entity.Property(a => a.Name)
                .HasColumnName("name")
                .IsRequired();

            entity.Property(a => a.CurrentMeterId)
                .HasColumnName("current_meter_id");
            
            entity.HasMany(a => a.MeterReplacementHistories)
                .WithOne(m => m.Apartment)
                .HasForeignKey(m => m.ApartmentId);
        });

        modelBuilder.Entity<MeterReplacementHistory> (entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Id).ValueGeneratedOnAdd();
            
            entity.Property(h => h.CreationDate)
                .HasColumnName("creation_date")
                .IsRequired();

            entity.Property(h => h.OldMeterReadingValue)
                .HasColumnName("old_meter_reading_value");
            
            entity.Property(h => h.ApartmentId)
                .HasColumnName("apartment_id")
                .IsRequired();

            entity.Property(h => h.NewMeterId)
                .HasColumnName("new_meter_id");
        });

        modelBuilder.Entity<Meter>().HasData(
            new
            {
                Id = 1,
                SerialNumber = "abc123-2023",
                LastReadingDate = new DateTime(2023, 4, 10),
                NextReadingDate = new DateTime(2023, 5, 10),
            },
            new
            {
                Id = 2,
                SerialNumber = "def456-2023",
                LastReadingDate = new DateTime(2023, 4, 5),
                NextReadingDate = new DateTime(2023, 5, 5),
            },
            new
            {
                Id = 3,
                SerialNumber = "ghi789-2022",
                LastReadingDate = new DateTime(2023, 4, 1),
                NextReadingDate = new DateTime(2023, 5, 1),
            },
            new
            {
                Id = 4,
                SerialNumber = "jkl123-2022",
            }
        );

        modelBuilder.Entity<MeterReading>().HasData(
            new
            {
                Id = 1,
                Value = 342.5,
                ReadingDate = new DateTime(2023, 4, 10),
                MeterId = 1,
            },
            new
            {
                Id = 2,
                Value = Convert.ToDouble(100),
                ReadingDate = new DateTime(2023, 4, 5),
                MeterId = 2,
            },
            new
            {
                Id = 3,
                Value = 32470.34,
                ReadingDate = new DateTime(2023, 4, 1),
                MeterId = 3,
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
            },
            new
            {
                Id = 3,
                Name = "Lenina/22/20",
                CurrentMeterId = 2,
            },
            new
            {
                Id = 4,
                Name = "Lenina/22/9",
                CurrentMeterId = 3,
            },
            new
            {
                Id = 5,
                Name = "Lenina/10/21",
            }
        );
    }
}