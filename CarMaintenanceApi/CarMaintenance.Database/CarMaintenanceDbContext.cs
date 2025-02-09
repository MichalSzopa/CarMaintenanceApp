using CarMaintenance.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance.Database;

public class CarMaintenanceDbContext(DbContextOptions<CarMaintenanceDbContext> options) : DbContext(options), ICarMaintenanceDbContext
{
	public DbSet<User> Users => Set<User>();
	public DbSet<Car> Cars => Set<Car>();
	public DbSet<CarAccess> CarAccesses => Set<CarAccess>();
	public DbSet<CarServicing> CarServices => Set<CarServicing>();
	public DbSet<Notification> Notifications => Set<Notification>();
	public DbSet<CarIssue> CarIssues => Set<CarIssue>();
	public DbSet<CarInsurance> Insurances => Set<CarInsurance>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Users
		modelBuilder.Entity<User>(entity =>
		{
			entity.ToTable("Users");
			entity.HasKey(e => e.Id);
		});

		// Cars
		modelBuilder.Entity<Car>(entity =>
		{
			entity.ToTable("Car");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Owner)
			  .WithMany(e => e.OwnedCars)
			  .HasForeignKey(e => e.OwnerId)
			  .OnDelete(DeleteBehavior.Restrict);
		});

		// CarAccess
		modelBuilder.Entity<CarAccess>(entity =>
		{
			entity.ToTable("CarAccess");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Car)
			  .WithMany(e => e.GrantedAccesses)
			  .HasForeignKey(e => e.CarId)
			  .OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.User)
			  .WithMany(e => e.GrantedCarAccesses)
			  .HasForeignKey(e => e.UserId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Services
		modelBuilder.Entity<CarServicing>(entity =>
		{
			entity.ToTable("CarService");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Car)
			  .WithMany(e => e.CarServices)
			  .HasForeignKey(e => e.CarId)
			  .OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.ServiceNotification)
			  .WithOne(e => e.CarService)
			  .HasForeignKey<Notification>(e => e.CarServiceId)
			  .OnDelete(DeleteBehavior.SetNull);
		});

		// Notifications
		modelBuilder.Entity<Notification>(entity =>
		{
			entity.ToTable("Notification");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Car)
			  .WithMany(e => e.Notifications)
			  .HasForeignKey(e => e.CarId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// CarIssues
		modelBuilder.Entity<CarIssue>(entity =>
		{
			entity.ToTable("CarIssue");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Car)
			  .WithMany(e => e.CarIssues)
			  .HasForeignKey(e => e.CarId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Insurances
		modelBuilder.Entity<CarInsurance>(entity =>
		{
			entity.ToTable("CarInsurance");
			entity.HasKey(e => e.Id);

			entity.HasOne(e => e.Car)
			  .WithMany(e => e.CarInsurances)
			  .HasForeignKey(e => e.CarId)
			  .OnDelete(DeleteBehavior.Cascade);
		});
	}
}
