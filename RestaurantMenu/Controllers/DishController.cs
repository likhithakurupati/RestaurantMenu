using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantMenu.Models;
using RestaurantMenu.Repositories.Abstract;
using RestaurantMenu.Services;

namespace RestaurantMenu.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IIngredientService _ingredientService;
        private readonly IFileService _fileService;
        public DishController(IDishService dishService, IFileService fileService, IIngredientService ingredientService)
        {
            _dishService = dishService;
            _fileService = fileService;
            _ingredientService = ingredientService;
        }
        public IActionResult Add()
        {
            var model = new Dish();
            model.IngredientList = _ingredientService.List().Select(a => new SelectListItem { Text = a.IngredientName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Dish model)
        {
            model.IngredientList = _ingredientService.List().Select(a => new SelectListItem { Text = a.IngredientName, Value = a.Id.ToString() });

            if (!ModelState.IsValid)
                return View(model);
            var folderPath = "Assets/Dishes";
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile, folderPath);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.DishImage = imageName;
            }
            var result = _dishService.Add(model);
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
            var model = _dishService.GetById(id);
            var selectedIngredients = _dishService.GetIngredientByDishId(model.Id);
            MultiSelectList multiIngredientList = new MultiSelectList(_ingredientService.List(), "Id", "IngredientName", selectedIngredients);
            model.MultiIngredientList = multiIngredientList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Dish model)
        {
            var selectedIngredients = _dishService.GetIngredientByDishId(model.Id);
            MultiSelectList multiIngredientList = new MultiSelectList(_ingredientService.List(), "Id", "IngredientName", selectedIngredients);
            model.MultiIngredientList = multiIngredientList;
            if (!ModelState.IsValid)
                return View(model);
            var folderPath = "Assets/Dishes";
            if (model.ImageFile != null)
            {
                var fileResult = this._fileService.SaveImage(model.ImageFile, folderPath);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.DishImage = imageName;
            }
            var result = _dishService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(DishList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult DishList()
        {
            var data = this._dishService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _dishService.Delete(id);
            return RedirectToAction(nameof(DishList));
        }


    }
}
