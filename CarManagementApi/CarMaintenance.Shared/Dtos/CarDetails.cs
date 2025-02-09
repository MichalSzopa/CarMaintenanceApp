namespace CarMaintenance.Shared.Dtos;

public class CarDetails
{
  public string Make { get; set; }
  public string Model { get; set; }
  public int Year { get; set; }
  public string VIN { get; set; }
  public string Owner { get; set; }
  public CarInsuranceBasicData Insurance { get; set; }
  // public List<UserBasicData> UsersWithAccess { get; set; }
}
