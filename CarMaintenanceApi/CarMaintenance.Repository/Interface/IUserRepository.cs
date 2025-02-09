using CarMaintenance.Database.Entities;

namespace CarMaintenance.Repository.Interface;

public interface IUserRepository
{
  Task AddUserAsync(User user);
}
