namespace CarMaintenance.Shared.Dtos.Car;

public class CarDetailsModel
{
	public string Make { get; set; }
	public string Model { get; set; }
	public int Year { get; set; }
	public string VIN { get; set; }
	public string Owner { get; set; }
	public int Mileage { get; set; }
	public CarInsuranceBasicDataModel Insurance { get; set; }
	// public List<UserBasicData> UsersWithAccess { get; set; }
}
