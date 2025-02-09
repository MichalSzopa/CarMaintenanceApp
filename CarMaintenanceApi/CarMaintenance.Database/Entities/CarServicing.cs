using CarMaintenance.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMaintenance.Database.Entities;

public class CarServicing
{
	public int Id { get; set; }

	[Required]
	public int CarId { get; set; }

	[Required]
	[StringLength(200)]
	public string Description { get; set; }

	[Required]
	public DateTime ServiceDate { get; set; }

	public int Mileage { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal Cost { get; set; }

	public EServiceType Type { get; set; }

	[StringLength(500)]
	public string Notes { get; set; }

	public virtual Car Car { get; set; }
	public virtual Notification ServiceNotification { get; set; }
}
