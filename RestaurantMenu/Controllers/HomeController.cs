using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.Models;
using RestaurantMenu.Repositories.Abstract;
using System.Diagnostics;

namespace RestaurantMenu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;
        public HomeController(IDishService dishService)
        {
            _dishService = dishService;
        }
        public IActionResult Index()
        {
            var movies = _dishService.List();
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MovieDetail(int movieId)
        {
            var movie = _dishService.GetById(movieId);
            return View(movie);
        }
    }
}
