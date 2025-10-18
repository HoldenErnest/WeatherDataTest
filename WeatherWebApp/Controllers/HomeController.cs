using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherWebApp.Models;

using System.Text.Json;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Net.Http.Headers;
using System.Text.Unicode;
using System.Text;

namespace WeatherWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private string apiToken = "";

    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _httpClient = httpClient;
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

        WeatherData? data = await GetWeatherData(model);

        if (data == null) { // data could not be fetched properly
            return Index();
        }
        
        HttpContext.Session.SetString("weatherData", JsonSerializer.Serialize(data));
        //TODO: Send to the data to the view
        return RedirectToAction("Weather");
    }

    public IActionResult Weather()
    {
        // TODO: Give an error message
        var dataRaw = HttpContext.Session.GetString("weatherData");
        if (dataRaw == null) return Index();

        var wd = JsonSerializer.Deserialize<WeatherData>(dataRaw);

        Console.WriteLine("Weather Page");
        return View(wd);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    // Refresh the API Token. Returns false if there is a problem fetching a new token
    private async Task<bool> RefreshToken()
    { 
        Console.WriteLine("Checking token");
        //TODO
        HttpResponseMessage response = await _httpClient.GetAsync("https://ami-interviewassessment.azurewebsites.net/Auth/AccessToken");
        if (response == null || !response.IsSuccessStatusCode) return false;

        string apiResponse = await response.Content.ReadAsStringAsync();
        AccessToken? aTokenObj = JsonSerializer.Deserialize<AccessToken>(apiResponse);
        if (aTokenObj == null)
        {
            Console.WriteLine("API TOKEN could not be parsed");
            return false;
        }
        apiToken = aTokenObj.accessToken;

        Console.WriteLine("New API TOKEN: " + apiToken);

        return true;
    }
    
    // returns the response from the API given an input WeatherData
    private async Task<WeatherData?> GetWeatherData(WeatherData wd)
    {

        if (apiToken == "") await RefreshToken();

        //"{ \"locations\": [" + JsonSerializer.Serialize(wd) + "]}"
        APIInput contentObj = new APIInput(wd, "F");

        StringContent content   = new StringContent(JsonSerializer.Serialize(contentObj), Encoding.UTF8, "application/json");

        // make API call
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
        HttpResponseMessage response = await _httpClient.PostAsync("https://ami-interviewassessment.azurewebsites.net/WeatherData/ByLocation", content);

        // if there is a problem with the API, try refreshing the API Token
        if (response == null || !response.IsSuccessStatusCode)
        {
            if (!await RefreshToken()) return null;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            response = await _httpClient.PostAsync("https://ami-interviewassessment.azurewebsites.net/WeatherData/ByLocation", content);


            if (response == null || !response.IsSuccessStatusCode)
            {
                Console.WriteLine("API response is bad");
                return null;
            }
        }

        // the response was successful.. parse the data
        string apiResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("RESPONSE!:: " + apiResponse);
        WeatherData[]? dataObj = JsonSerializer.Deserialize<WeatherData[]>(apiResponse);
        if (dataObj == null || dataObj.Length < 1) {
            Console.WriteLine("Problem Parsing the Weather Data");
            return null;
        }

        return dataObj[0];
    }
}
