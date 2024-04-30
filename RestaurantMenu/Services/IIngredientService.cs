using Humanizer.Localisation;
using RestaurantMenu.Models;

namespace RestaurantMenu.Services
{
    public interface IIngredientService
    {
        bool Add(Ingredient model);
        bool Update(Ingredient model);
        Ingredient GetById(int id);
        List<string> GetIngredientNamesByIds(List<int> id);
        bool Delete(int id);
        IQueryable<Ingredient> List();
    }
}
