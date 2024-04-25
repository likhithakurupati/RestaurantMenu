using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.Models;
using RestaurantMenu.Repositories.Abstract;

namespace RestaurantMenu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientController : Controller
    {
       private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Ingredient model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _ingredientService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _ingredientService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Ingredient model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _ingredientService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(IngredientList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult IngredientList()
        {
            var data = this._ingredientService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _ingredientService.Delete(id);
            return RedirectToAction(nameof(IngredientList));
        }

    }
}
