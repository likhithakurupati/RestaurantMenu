using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string? IngredientName { get; set; }
    }
}
