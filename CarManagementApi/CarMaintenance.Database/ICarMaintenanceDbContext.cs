using CarMaintenance.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenance.Database;

public interface ICarMaintenanceDbContext
{
  DbSet<User> Users { get; }
  DbSet<Car> Cars { get; }
  DbSet<CarAccess> CarAccesses { get; }
  DbSet<CarServicing> CarServices { get; }
  DbSet<Notification> Notifications { get; }
  DbSet<CarIssue> CarIssues { get; }
  DbSet<CarInsurance> Insurances { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
