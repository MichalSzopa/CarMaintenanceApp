using CarMaintenance.Database;
using CarMaintenance.Repository.Interface;
using CarMaintenance.Repository.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarMaintenance.Repository;

public class UnitOfWork(CarMaintenanceDbContext context) : IUnitOfWork
{
  private IDbContextTransaction? _transaction;
  private bool _disposed;

  private IUserRepository? _users { get; set; }
  private ICarRepository? _cars { get; set; }
  private INotificationRepository? _notifications { get; set; }
  private ICarIssueRepository? _carIssues { get; set; }
  private ICarInsuranceRepository? _carInsurances { get; set; }

  public IUserRepository Users
  {
    get
    {
      _users ??= new UserRepository(context);
      return _users;
    }
  }

  public ICarRepository Cars
  {
    get
    {
      _cars ??= new CarRepository(context);
      return _cars;
    }
  }
  public INotificationRepository Notifications
  {
    get
    {
      _notifications ??= new NotificationRepository(context);
      return _notifications;
    }
  }
  public ICarIssueRepository CarIssues
  {
    get
    {
      _carIssues ??= new CarIssueRepository(context);
      return _carIssues;
    }
  }
  public ICarInsuranceRepository CarInsurances
  {
    get
    {
      _carInsurances ??= new CarInsuranceRepository(context);
      return _carInsurances;
    }
  }


  public async Task<int> SaveChangesAsync()
  {
    return await context.SaveChangesAsync();
  }

  public async Task BeginTransactionAsync()
  {
    _transaction = await context.Database.BeginTransactionAsync();
  }

  public async Task CommitAsync()
  {
    try
    {
      await context.SaveChangesAsync();
      await _transaction?.CommitAsync()!;
    }
    catch
    {
      await RollbackAsync();
      throw;
    }
    finally
    {
      _transaction?.Dispose();
      _transaction = null;
    }
  }

  public async Task RollbackAsync()
  {
    if (_transaction != null)
    {
      await _transaction.RollbackAsync();
      _transaction.Dispose();
      _transaction = null;
    }
  }

  public void Dispose()
  {
    if (!_disposed)
    {
      _transaction?.Dispose();
      context.Dispose();
      _disposed = true;
    }
    GC.SuppressFinalize(this);
  }
}
