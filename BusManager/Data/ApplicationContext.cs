using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;
using static BusManager.Repository.StationRepository;

namespace BusManager.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Stop> Stop { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<StationDTO> StationDto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=ep-hidden-cell-a5kujchj.us-east-2.aws.neon.tech;Database=busmanager;User Id=busmanager_owner;Password=VNT57sorQIup;Port=5432");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(p =>
            {
                p.ToTable("Buses");
                p.HasKey(p => p.Id);
                p.Property(p => p.Model).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(p => p.Plate).HasColumnType("VARCHAR(10)").IsRequired();
                p.Property(p => p.Active).HasColumnType("INTEGER").IsRequired();

                p.HasIndex(p => p.Id);
                p.HasIndex(p => p.Plate);
            });

            modelBuilder.Entity<Driver>(p =>
            {
                p.ToTable("Drivers");
                p.HasKey(p => p.Id);
                p.Property(p => p.CPF).HasColumnType("VARCHAR(14)").IsRequired();
                p.Property(p => p.Name).HasColumnType("VARCHAR(15)").IsRequired();
                p.Property(p => p.Surname).HasColumnType("VARCHAR(15)").IsRequired();
                p.Property(p => p.Birthday).HasColumnType("DATE").IsRequired();
                p.Property(p => p.HireDate).HasColumnType("DATE").IsRequired();
                p.Property(p => p.Active).HasColumnType("INTEGER").IsRequired();

                p.HasIndex(p => p.Id);
                p.HasIndex(p => p.CPF);
            });

            modelBuilder.Entity<Station>(p =>
            {
                p.ToTable("Stations");
                p.HasKey(p => p.Id);

                p.Property(p => p.Address).HasColumnType("VARCHAR(100)").IsRequired();
                p.Property(p => p.Number).HasColumnType("VARCHAR(10)").IsRequired();
                p.Property(p => p.Active).HasColumnType("INTEGER").IsRequired();

                p.HasMany(station => station.Stops)
                 .WithOne(stop => stop.Station)
                 .HasForeignKey(stop => stop.StationId);

                p.HasIndex(p => p.Id);
            });

            modelBuilder.Entity<Line>(p =>
            {
                p.ToTable("Lines");
                p.HasKey(p => p.Id);

                p.Property(p => p.Name).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(p => p.Code).HasColumnType("VARCHAR(10)").IsRequired();
                p.Property(p => p.Origin).HasColumnType("VARCHAR(20)").IsRequired();
                p.Property(p => p.Destiny).HasColumnType("VARCHAR(20)").IsRequired();
                p.Property(p => p.Description).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(p => p.Active).HasColumnType("INTEGER").IsRequired();

                p.HasMany(line => line.Stops)
                 .WithOne(stop => stop.Line)
                 .HasForeignKey(stop => stop.LineId);

                p.HasIndex(p => p.Id);
                p.HasIndex(p => p.Code);
            });

            modelBuilder.Entity<Stop>(p =>
            {
                p.ToTable("Stops");

                p.HasKey(stop => new { stop.LineId, stop.StationId });

                p.HasOne(stop => stop.Line)
                 .WithMany(line => line.Stops)
                 .HasForeignKey(stop => stop.LineId)
                 .OnDelete(DeleteBehavior.Restrict);

                p.HasOne(stop => stop.Station)
                 .WithMany(station => station.Stops)
                 .HasForeignKey(stop => stop.StationId)
                 .OnDelete(DeleteBehavior.Restrict);

                p.Property(stop => stop.Hour).IsRequired();

                p.HasIndex(stop => new { stop.LineId, stop.StationId }).IsUnique();
            });

            modelBuilder.Entity<Trip>(p =>
            {
                p.ToTable("Trips");
                p.HasKey(p => p.Id);

                p.Property(p => p.StartTime).HasColumnType("DATE").IsRequired();
                p.Property(p => p.EndTime).HasColumnType("DATE").IsRequired();

                p.HasOne(trip => trip.Bus)
                 .WithMany()
                 .HasForeignKey(trip => trip.BusId)
                 .IsRequired();

                p.HasOne(trip => trip.Driver)
                 .WithMany()
                 .HasForeignKey(trip => trip.DriverId)
                 .IsRequired();

                p.HasOne(trip => trip.Line)
                 .WithMany()
                 .HasForeignKey(trip => trip.LineId)
                 .IsRequired();

                p.HasIndex(p => p.Id);
                p.HasIndex(p => p.BusId);
                p.HasIndex(p => p.DriverId);
                p.HasIndex(p => p.LineId);
            });
        }
    }
}
