using CarMaintenance.Database;
using CarMaintenance.Repository.Interface;

namespace CarMaintenance.Repository.Repository;

public class CarInsuranceRepository(ICarMaintenanceDbContext dbContext) : ICarInsuranceRepository
{
}
