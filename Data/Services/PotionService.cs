﻿using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
                int index = -1;
                int counter = 0;
                foreach (var ingredient in potion.Ingredients)
                {
                    index++;
                    if (ingredient.Name == recipe.Ingredients[index].Name)
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
            var name = $"{brewer.Name}'s {potionStatus}";
           
            if (potionStatus == BrewingStatus.Discovery)
            {
                var newRecipe = new Recipe()
                {
                    Ingredients = potion.Ingredients,
                    Name = name,
                    student = brewer
                };

                var newPotion = new Potion()
                {
                    Name = name,
                    Recipe = newRecipe,
                    Ingredients = newRecipe.Ingredients,
                    Student = brewer,
                    BrewingStatus = potionStatus
                };
                _context.Potions.Add(newPotion);
            } else if (potionStatus == BrewingStatus.Replica)
            {
                var newPotion = new Potion()
                {
                    Name = name,
                    Recipe = recipeIfReplica,
                    Ingredients = potion.Ingredients,
                    Student = brewer,
                    BrewingStatus = potionStatus
                };
                _context.Potions.Add(newPotion);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Potion> GetPotion(long potionId)
        {
            return await _context.Potions.Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(r => r.ID == potionId);
        }

        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.Potions.Include(p => p.Ingredients)
                .Include(p =>p.Recipe).ToListAsync();
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

        public Task<Student> GetStudent(long studentId)
        {
            var student = _context.Students.Include(student => student.Room).ToListAsync().Result
                .FirstOrDefault(student => student.ID == studentId);
            return Task.FromResult(student);
        }
    }
}
