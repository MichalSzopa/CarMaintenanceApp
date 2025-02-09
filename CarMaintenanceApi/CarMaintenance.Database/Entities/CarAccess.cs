using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Database.Entities;

public class CarAccess
{
  public int Id { get; set; }

  [Required]
  public int CarId { get; set; }

  [Required]
  public int UserId { get; set; }

  [Required]
  public DateTime GrantedDate { get; set; }

  public DateTime? ExpiryDate { get; set; }

  public virtual Car Car { get; set; }
  public virtual User User { get; set; }
}
