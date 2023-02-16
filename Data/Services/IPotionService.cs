using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Data.Services
{
    public interface IPotionService
    {
        public Task<Potion> AddPotion(Potion potion);
        public Task<Potion> GetPotion(long potionId);
        public Task<List<Potion>> GetAllPotions();
        public Task UpdatePotion(long id, Potion potion);
        public Task DeletePotion(long id);
        public Task<List<Potion>> GetAllPotionsOfAStudent(long id);
        public Task<Potion> BrewPotion(long id);
        public Task<Potion> AddIngredientToPotion(long id, Ingredient ingredient);
        public Task<List<Recipe>> GetRecipesThatHasTheAddedIngredients(long id);
    }
}
