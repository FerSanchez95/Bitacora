using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bitacora.Models;
using Microsoft.AspNetCore.Identity;
using Bitacora.Auth;

namespace Bitacora.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity is not null && !User.Identity.IsAuthenticated)
        {
			return RedirectToAction("IniciarSesion", "Account");
			
        }
        return RedirectToAction("Index", "Bitacoras");

	}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
