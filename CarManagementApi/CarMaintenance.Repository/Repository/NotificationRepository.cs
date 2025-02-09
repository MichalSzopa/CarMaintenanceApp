using CarMaintenance.Database;
using CarMaintenance.Repository.Interface;

namespace CarMaintenance.Repository.Repository;

public class NotificationRepository(ICarMaintenanceDbContext dbContext) : INotificationRepository
{
}
