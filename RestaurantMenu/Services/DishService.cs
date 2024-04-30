using RestaurantMenu.Data;
using RestaurantMenu.Data.Migrations;
using RestaurantMenu.Models;
using DishIngredient = RestaurantMenu.Models.DishIngredient;

namespace RestaurantMenu.Services
{
    public class DishService : IDishService
    {
        private readonly ApplicationDbContext ctx;
        public DishService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Dish model)
        {
            try
            {
                ctx.Dish.Add(model);
                ctx.SaveChanges();
                foreach (int ingredientId in model.Ingredients)
                {
                    var dishIngredient = new DishIngredient
                    {
                        DishId = model.Id,
                        IngredientId = ingredientId
                    };
                    ctx.DishIngredient.Add(dishIngredient);
                }
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
                var dishIngredients = ctx.DishIngredient.Where(a => a.DishId == data.Id);
                foreach (var dishIngredient in dishIngredients)
                {
                    ctx.DishIngredient.Remove(dishIngredient);
                }
                ctx.Dish.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Dish GetById(int id)
        {
            return ctx.Dish.Find(id);
        }

        public DishListViewModel List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new DishListViewModel();
            var list = ctx.Dish.ToList();
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().Contains(term)).ToList();
            }
            if (paging)
            {
                // here we will apply paging
                int pageSize = 4;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }
            foreach (var dish in list)
            {
                var ingredients = (from ingredient in ctx.Ingredient
                                   join di in ctx.DishIngredient
                                   on ingredient.Id equals di.IngredientId
                                   where di.DishId == dish.Id
                                   select ingredient.IngredientName
                              ).ToList();
                var ingredientNames = string.Join(", ", ingredients);
                dish.IngredientNames = ingredientNames;
            }
            data.DishList = list.AsQueryable();
            return data;
        }

        public bool Update(Dish model)
        {
            try
            {
                var ingredientsToDelete = ctx.DishIngredient.Where(a => a.DishId == model.Id && !model.Ingredients.Contains(a.IngredientId)).ToList();
                foreach (var ingRem in ingredientsToDelete)
                {
                    ctx.DishIngredient.Remove(ingRem);
                }
                foreach (int ingId in model.Ingredients)
                {
                    var dishIngredient = ctx.DishIngredient.FirstOrDefault(a => a.DishId == model.Id && a.IngredientId == ingId);
                    if (dishIngredient == null)
                    {
                        dishIngredient = new DishIngredient { IngredientId = ingId, DishId = model.Id };
                        ctx.DishIngredient.Add(dishIngredient);
                    }
                }

                ctx.Dish.Update(model);
                // adds ingredient ids in dishIngredient table
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetIngredientByDishId(int dishId)
        {
            var ingredientIds = ctx.DishIngredient.Where(a => a.DishId == dishId).Select(a => a.IngredientId).ToList();
            return ingredientIds;
        }
    }
}
