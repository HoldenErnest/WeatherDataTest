namespace WeatherWebApp.Models;

[Serializable]
public class PrecipitationData
{
    required public string type { get; set; }
    public float probability { get; set; }

}
