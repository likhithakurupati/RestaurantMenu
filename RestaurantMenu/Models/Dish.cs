using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMenu.Models
{
    public class Dish
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public string? DishImage { get; set; }  // stores dishImage name with extension (eg, Mozzarella.jpg)
        [Required]
        public double Price { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Ingredients { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? IngredientList { get; set; }

        [NotMapped]
        public string? IngredientNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiIngredientList { get; set; }

    }
}
