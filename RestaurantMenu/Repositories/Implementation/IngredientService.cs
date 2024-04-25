using Humanizer.Localisation;
using RestaurantMenu.Data;
using RestaurantMenu.Models;
using RestaurantMenu.Repositories.Abstract;

namespace RestaurantMenu.Repositories.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext ctx;
        public IngredientService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Ingredient model)
        {
            try
            {
                ctx.Ingredient.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Ingredient.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Ingredient GetById(int id)
        {
            return ctx.Ingredient.Find(id);
        }

        public IQueryable<Ingredient> List()
        {
            var data = ctx.Ingredient.AsQueryable();
            return data;
        }

        public bool Update(Ingredient model)
        {
            try
            {
                ctx.Ingredient.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
