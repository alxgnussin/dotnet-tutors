using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutors.Models;
using Tutors.Services;

namespace Tutors.Controllers
{
    public class HomeController : Controller
    {
        private DataService _dataService;
        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }
        public IActionResult Index()
        {
            Random rng = new Random();
            List<Teacher> result = _dataService.GetTeachers().OrderBy(a => rng.Next()).Take(5).ToList();
            return View(result);
        }
    }
}
