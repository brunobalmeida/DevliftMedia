using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EventApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EvenAppContext _context;

        public HomeController(EvenAppContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var maxEventId = _context.Events.Max(a => a.EventId);

            HttpContext.Session.SetString("maxEventId", maxEventId.ToString());

            var eventAppContext = _context.Events.OrderBy(a=>a.EventDate);

            return View(await eventAppContext.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
