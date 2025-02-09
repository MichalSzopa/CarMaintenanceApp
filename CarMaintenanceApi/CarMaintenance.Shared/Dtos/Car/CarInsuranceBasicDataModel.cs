namespace CarMaintenance.Shared.Dtos.Car;

public class CarInsuranceBasicDataModel
{
	public string Provider { get; set; }
	public string PolicyNumber { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}
