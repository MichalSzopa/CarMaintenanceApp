using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Database.Entities;

public class User
{
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Email { get; set; }

	[Required]
	[StringLength(50)]
	public string FirstName { get; set; }

	[Required]
	[StringLength(50)]
	public string LastName { get; set; }

	[Required]
	[StringLength(255)]
	public string Password { get; set; }

	public string? RefreshToken { get; set; }

	public DateTime? RefreshTokenExpiry { get; set; }

	public virtual ICollection<Car> OwnedCars { get; set; }
	public virtual ICollection<CarAccess> GrantedCarAccesses { get; set; }
}