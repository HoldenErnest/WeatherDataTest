using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private string apiToken = "";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Console.WriteLine("Index page req");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(WeatherData model)
    {
        // Access form data through the model object
        
        if (!await RefreshToken())
            return Index(); //TODO: Give them an error message

        WeatherData data = await GetWeatherData(model);
        //TODO: Send to the data to the view

        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<bool> RefreshToken() { // returns false if there is a problem fetching a new token
        Console.WriteLine("Checking token");
        //TODO
        return false;
    }
    private async Task<WeatherData> GetWeatherData(WeatherData wd) { // returns the response from the API given a (partially completed) WeatherData
        string city = wd.city!;
        string state = wd.state!;
        string zip = wd.zip != null ? wd.zip : "";

        Console.WriteLine("Searching for :: Name: " + city + ", State: " + state + ", Zip: " + zip);

        //TODO
        return new WeatherData();
    }
}
