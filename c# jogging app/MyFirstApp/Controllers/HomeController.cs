using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;
using OsmSharp.Streams;
using System.Diagnostics;
using System.IO;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Map()
        {
            var fileStream = System.IO.File.OpenRead("./data/germany-latest.osm.pbf");
            // create source stream.
            var source = new PBFOsmStreamSource(fileStream);

            // let's use linq to leave only objects last touched by the given mapper.
            var filtered = from osmGeo in source
                           where
              osmGeo.UserName == "Javier Gutiérrez"
                           select osmGeo;

            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Message = "Security is very important";
            ViewBag.MyFavoriteColor = "Orange";
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
