namespace WeatherWebApp.Models;

[Serializable]
public class WeatherData
{

    public string? city { get; set; }
    public string? zip { get; set; }
    public string? state { get; set; }
    public int[]? rolling12MonthTemps;
    public int temperature { get; set; }
    public int windSpeed { get; set; }
    public float windDirection { get; set; }
    public float cloudCoverage { get; set; }
    public PrecipitationData[] precipitation { get; set; }
}
