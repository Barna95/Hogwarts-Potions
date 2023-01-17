using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace HogwartsPotions.Data
{
    public class DbInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HogwartsContext>();
                context.Database.EnsureCreated();
    //Rooms -----------------------------------------------
                var room1 = new Room()
                {
                    Capacity = 4,
                    Residents = new HashSet<Student>()
                };

                var room2 = new Room()
                {
                    Capacity = 4,
                    Residents = new HashSet<Student>()
                };

                var room3 = new Room()
                {
                    Capacity = 4,
                    Residents = new HashSet<Student>()
                };

                var room4 = new Room()
                {
                    Capacity = 4,
                    Residents = new HashSet<Student>()
                };
    // Students --------------------------------------------------
                var Jóska = new Student()
                {
                    Name = "Jóska",
                    HouseType = HouseType.Gryffindor,
                    PetType = PetType.Rat,
                    Room = null
                };
                var Pista = new Student()
                {
                    Name = "Pista",
                    HouseType = HouseType.Slytherin,
                    PetType = PetType.None,
                    Room = null
                };
                var Mari = new Student()
                {
                    Name = "Mari",
                    HouseType = HouseType.Ravenclaw,
                    PetType = PetType.Owl,
                    Room = null
                };
                var Maris = new Student()
                {
                    Name = "Maris",
                    HouseType = HouseType.Hufflepuff,
                    PetType = PetType.Rat,
                    Room = null
                };
                room1.Residents.Add(Pista);
                room2.Residents.Add(Mari);
                room2.Residents.Add(Maris);
                room1.Residents.Add(Jóska);
    //Ingredients ---------------------------------------------------
                var ginger = new Ingredient()
                {
                    Name = "Ginger"
                };
                var catFur = new Ingredient()
                {
                    Name = "CatFur"
                };
                var cannabis = new Ingredient()
                {
                    Name = "Cannabis"
                };
                var doomWeed = new Ingredient()
                {
                    Name = "DoomWeed"
                };
                var branchOfLife = new Ingredient()
                {
                    Name = "BranchOfLife"
                };
                var dragonScale = new Ingredient()
                {
                    Name = "DragonScale"
                }; var paprika = new Ingredient()
                {
                    Name = "Paprika"
                };
    //Recipe ------------------------------------------------------
                var jóskaRecipe = new Recipe()
                {
                    Name = "Jóska's discovery#1",
                    student = Jóska,
                    Ingredients = new List<Ingredient>(),
                };
                jóskaRecipe.Ingredients.Add(paprika);
                jóskaRecipe.Ingredients.Add(branchOfLife);
                jóskaRecipe.Ingredients.Add(dragonScale);
                jóskaRecipe.Ingredients.Add(doomWeed);
                jóskaRecipe.Ingredients.Add(ginger);

                //Potion ---------------------------------------------------------
                var jóskaPotion = new Potion()
                {
                    BrewingStatus = BrewingStatus.Discovery,
                    Ingredients = new List<Ingredient>(),
                    Name = "Jóska Potion",
                    Recipe = jóskaRecipe,
                    Student = Jóska
                };
                jóskaPotion.Ingredients.Add(paprika);
                jóskaPotion.Ingredients.Add(branchOfLife);
                jóskaPotion.Ingredients.Add(dragonScale);
                jóskaPotion.Ingredients.Add(doomWeed);
                jóskaPotion.Ingredients.Add(ginger);

                if (!context.Students.Any())
                {
                    
                    context.Students.AddRange(new List<Student>()
                    {
                        Jóska,
                        Pista,
                        Mari,
                        Maris
                    });
                    context.SaveChanges();
                }
                if (!context.Rooms.Any())
                {

                    context.Rooms.AddRange(new List<Room>()
                    {
                        room1,
                        room2,
                        room3,
                        room4,
                    });
                    context.SaveChanges();
                }
                if (!context.Ingredients.Any())
                {
                    context.Ingredients.AddRange(new List<Ingredient>()
                    {
                        ginger,
                        catFur,
                        cannabis,
                        doomWeed,
                        branchOfLife,
                        dragonScale,
                        paprika,

                    });
                    context.SaveChanges();
                }

                if (!context.Recipes.Any())
                {
                    context.Recipes.AddRange(new List<Recipe>()
                    {
                        jóskaRecipe
                    });
                    context.SaveChanges();
                }

                if (!context.Potions.Any())
                {
                    context.Potions.AddRange(new List<Potion>()
                    {
                        jóskaPotion
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
