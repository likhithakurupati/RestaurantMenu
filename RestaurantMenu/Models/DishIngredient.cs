namespace RestaurantMenu.Models
{
    public class DishIngredient
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int IngredientId { get; set; }
    }
}
