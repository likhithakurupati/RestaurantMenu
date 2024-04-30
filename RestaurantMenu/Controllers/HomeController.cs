using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantMenu.Models;
using RestaurantMenu.Services;
using System.Diagnostics;

namespace RestaurantMenu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IIngredientService _ingredientService;
        public HomeController(IDishService dishService, IIngredientService ingredientService)
        {
            _dishService = dishService;
            _ingredientService = ingredientService;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _dishService.List(term, true, currentPage);
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult DishDetail(int dishId)
        {
            var dish = _dishService.GetById(dishId);
            var ingredientIds = _dishService.GetIngredientByDishId(dishId);
            var ingredientNames = _ingredientService.GetIngredientNamesByIds(ingredientIds);
            dish.IngredientNames = string.Join(", ", ingredientNames);
            return View(dish);
        }
    }
}
