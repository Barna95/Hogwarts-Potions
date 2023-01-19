using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;


namespace HogwartsPotions.Data.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;
        public PotionService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task AddPotion(Potion potion)
        {
            var potionStatus = BrewingStatus.Discovery;
            var studentID = potion.Student.ID;
            var brewer = GetStudent(studentID).Result;
            var allRecipes = _context.Recipes.Include(recipe => recipe.Ingredients);
            Recipe recipeIfReplica = null;
            foreach (var recipe in allRecipes)
            {
                int counter = 0;
                foreach (var ingredient in potion.Ingredients)
                {
                    var ingredientExists = GetIngredientChecked(recipe, ingredient);
                    if (ingredientExists)
                    {
                        counter++;
                    }
                }

                if (counter == 5)
                {
                     potionStatus = BrewingStatus.Replica;
                     recipeIfReplica = recipe;
                }
            }

            if (potionStatus == BrewingStatus.Discovery)
            {
                var newRecipe = new Recipe()
                {
                    Ingredients = potion.Ingredients,
                    student = brewer
                };

                var newPotion = new Potion()
                {
                    Recipe = newRecipe,
                    Ingredients = ConnectIngredients(newRecipe.Ingredients),
                    Student = brewer,
                    BrewingStatus = potionStatus
                };
                var name = $"{brewer.Name}'s {potionStatus} #{PotionCounter(newPotion)}";
                newPotion.Name = name;
                newPotion.Recipe.Name = name;
                _context.Potions.Add(newPotion);
            } else if (potionStatus == BrewingStatus.Replica)
            {
                var newPotion = new Potion()
                {
                    Recipe = recipeIfReplica,
                    Ingredients = ConnectIngredients(recipeIfReplica.Ingredients),
                    Student = brewer,
                    BrewingStatus = potionStatus
                };
                var name = $"{brewer.Name}'s {potionStatus} #{PotionCounter(newPotion)}";
                newPotion.Name = name;
                newPotion.Recipe.Name = name;
                _context.Potions.Add(newPotion);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Potion> GetPotion(long potionId)
        {
            return await _context.Potions.Include(p => p.Ingredients)
                .FirstOrDefaultAsync(r => r.ID == potionId);
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.Potions.Include(p => p.Ingredients)
                .Include(p =>p.Recipe)
                .Include(p => p.Student).AsNoTracking().ToListAsync();
        }

        public async Task UpdatePotion(long id, Potion potion)
        {
            var updatePotion = _context.Potions.FirstOrDefault(r => r.ID == id);
            updatePotion.Ingredients = potion.Ingredients;
            updatePotion.Name = potion.Name;
            updatePotion.Student = potion.Student;
            updatePotion.BrewingStatus = potion.BrewingStatus;
            updatePotion.Recipe = potion.Recipe;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePotion(long id)
        {
            var deletePotion = await _context.Potions.FirstOrDefaultAsync(r => r.ID == id);
            _context.Potions.Remove(deletePotion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Potion>> GetAllPotionsOfAStudent(long id)
        {
            return await _context.Potions.Where(pot => pot.Student.ID == id).Include(p => p.Recipe).ToListAsync();
        }

        public async Task<Potion> BrewPotion(long id)
        {
            var newPotion = new Potion();
            var student = GetStudent(id).Result;
            newPotion.Name = $"{student.Name}'s Potion";
            newPotion.Student = student;
            newPotion.BrewingStatus = BrewingStatus.Brew;
            _context.Potions.Add(newPotion);
            await _context.SaveChangesAsync();
            return newPotion;
        }

        // Helpers----------------------------------------------------
        public Task<Student> GetStudent(long studentId)
        {
            var student = _context.Students
                .FirstOrDefault(student => student.ID == studentId);
            return Task.FromResult(student);
        }

        public bool GetIngredientChecked(Recipe recipe, Ingredient ingredient)
        {
            var theIngredient = recipe.Ingredients.FirstOrDefault(ing => ing.Name.Equals(ingredient.Name));
            return theIngredient != null;
        }

        public HashSet<Ingredient> ConnectIngredients(HashSet<Ingredient> ingredients)
        {
            var newSet = new HashSet<Ingredient>();
            foreach (var ingredient in ingredients)
            {
                newSet.Add(_context.Ingredients.FirstOrDefault(ing => ing.Name == ingredient.Name));
            }
            return newSet;
        }

        private int PotionCounter(Potion potion)
        {
            var baseIndex = 1;
            var count = _context.Potions.Count(p =>
                p.Student == potion.Student &&
                p.BrewingStatus == potion.BrewingStatus) + baseIndex;
            return count;
        }
    }
}
