using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Console.WriteLine("Index page req");
        return View();
    }

    public IActionResult Index2()
    {
        Console.WriteLine("Test (Index2)");
        return View();
    }

    public IActionResult Privacy()
    {
        Console.WriteLine("Privacy page req");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
