namespace RestaurantMenu.Models
{
    public class DishListViewModel
    {
        public IQueryable<Dish> DishList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
