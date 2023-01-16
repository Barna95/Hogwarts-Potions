using Microsoft.AspNetCore.Builder;
using System;
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

                if (!context.Students.Any())
                {
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

               
            }
        }
    }
}
