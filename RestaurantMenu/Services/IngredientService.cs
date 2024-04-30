using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Data;
using RestaurantMenu.Models;

namespace RestaurantMenu.Services
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
                var data = GetById(id);
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

        public List<string> GetIngredientNamesByIds(List<int> ingredientIds)
        {
            return ctx.Ingredient.AsQueryable()
                .Where(i => ingredientIds.Contains(i.Id))
                .Select(i => i.IngredientName)
                .ToList();
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
