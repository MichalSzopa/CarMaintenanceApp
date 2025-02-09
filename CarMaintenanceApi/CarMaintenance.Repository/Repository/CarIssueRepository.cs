using CarMaintenance.Database;
using CarMaintenance.Repository.Interface;

namespace CarMaintenance.Repository.Repository;

public class CarIssueRepository(ICarMaintenanceDbContext dbContext) : ICarIssueRepository
{
}
