using System.Diagnostics;
using Bestes_Robo_Figma_Projekt.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bestes_Robo_Figma_Projekt.Controllers
{
    public class ByteBuddyController : Controller
    {
        public IActionResult Startgame()
        {
            return View();
        }
    }
}