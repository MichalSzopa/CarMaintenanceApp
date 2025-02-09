namespace CarMaintenance.Database.Entities;

public class Notification
{
	public int Id { get; set; }

	public int CarId { get; set; }

	public int CarServiceId { get; set; }

	public bool NotifyAllUsers { get; set; }

	public virtual Car Car { get; set; }
	public virtual CarServicing CarService { get; set; }
}
