using RestaurantMenu.Models;

namespace RestaurantMenu.Services
{
    public interface IDishService
    {
        bool Add(Dish model);
        bool Update(Dish model);
        Dish GetById(int id);
        bool Delete(int id);
        DishListViewModel List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetIngredientByDishId(int dishId);
    }
}
