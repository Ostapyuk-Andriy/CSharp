﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurveyTwo.Models;

namespace DojoSurveyTwo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost("/result")]
    public IActionResult Submit(Survey newSurvey)
    {
        if(!ModelState.IsValid)
        {
            return View("Index");
        }
        return View("Result", newSurvey);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
