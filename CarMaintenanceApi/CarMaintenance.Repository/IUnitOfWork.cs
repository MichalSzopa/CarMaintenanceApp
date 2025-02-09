using CarMaintenance.Repository.Interface;

namespace CarMaintenance.Repository;

public interface IUnitOfWork : IDisposable
{
	IUserRepository Users { get; }
	ICarRepository Cars { get; }
	// IGenericRepository<Service> Services { get; }
	INotificationRepository Notifications { get; }
	ICarIssueRepository CarIssues { get; }
	ICarInsuranceRepository CarInsurances { get; }

	Task<int> SaveChangesAsync();
	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
}
