using CarMaintenance.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Database.Entities;

public class CarIssue
{
  public int Id { get; set; }

  [Required]
  public int CarId { get; set; }

  [Required]
  [StringLength(200)]
  public string Title { get; set; }

  [Required]
  [StringLength(1000)]
  public string Description { get; set; }

  [Required]
  public DateTime ReportedDate { get; set; }

  public DateTime? ResolvedDate { get; set; }

  public EIssueStatus Status { get; set; }

  public EIssueUrgency Urgency { get; set; }

  public virtual Car Car { get; set; }
}
