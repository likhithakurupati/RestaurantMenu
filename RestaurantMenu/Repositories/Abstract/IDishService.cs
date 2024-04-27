using RestaurantMenu.Models;

namespace RestaurantMenu.Repositories.Abstract
{
    public interface IDishService
    {
        bool Add(Dish model);
        bool Update(Dish model);
        Dish GetById(int id);
        bool Delete(int id);
        DishListViewModel List();
        List<int> GetIngredientByDishId(int dishId);
    }
}
