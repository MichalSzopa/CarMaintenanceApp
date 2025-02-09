using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Database.Entities;

public class CarInsurance
{
  public int Id { get; set; }

  [Required]
  public int CarId { get; set; }

  [Required]
  [StringLength(100)]
  public string Provider { get; set; }

  [Required]
  [StringLength(50)]
  public string PolicyNumber { get; set; }

  [Required]
  public DateTime StartDate { get; set; }

  [Required]
  public DateTime EndDate { get; set; }

  public virtual Car Car { get; set; }
}
