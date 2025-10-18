namespace WeatherWebApp.Models;

[Serializable]
public class APIInput
{
    public APIInput(WeatherData loc, string uom)
    {
        locations = new WeatherData[1];
        locations[0] = loc;
        unitOfMeasurement = uom;
    }

    public WeatherData[] locations { get; set; }
    public string unitOfMeasurement = "F";
}
