using Humanizer.Localisation;
using RestaurantMenu.Models;

namespace RestaurantMenu.Repositories.Abstract
{
    public interface IIngredientService
    {
        bool Add(Ingredient model);
        bool Update(Ingredient model);
        Ingredient GetById(int id);
        bool Delete(int id);
        IQueryable<Ingredient> List();
    }
}
