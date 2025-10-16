namespace WeatherWebApp.Models;

public class WeatherData
{

    public string? city { get; set; }
    public string? zip { get; set; }
    public string? state { get; set; }
    public int[]? rolling12MonthTemps;
    public int temperature = 0;
    public int windSpeed = 0;
    public float windDirection = 0.0f;
    public float cloudCoverage = 0.0f;
    public PrecipitationData[] precipitation = { new PrecipitationData() };

    public override string ToString()
    {
        return "Later imp..";
    }
}
