using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Database.Entities;

public class Car
{
  public int Id { get; set; }

  [Required]
  [StringLength(50)]
  public string Make { get; set; }

  [Required]
  [StringLength(50)]
  public string Model { get; set; }

  public int Year { get; set; }

  [StringLength(17)]
  public string VIN { get; set; }

  [Required]
  public int OwnerId { get; set; }

  public virtual User Owner { get; set; }
  public virtual ICollection<CarAccess> GrantedAccesses { get; set; }
  public virtual ICollection<CarServicing> CarServices { get; set; }
  public virtual ICollection<Notification> Notifications { get; set; }
  public virtual ICollection<CarIssue> CarIssues { get; set; }
  public virtual ICollection<CarInsurance> CarInsurances { get; set; }
}
