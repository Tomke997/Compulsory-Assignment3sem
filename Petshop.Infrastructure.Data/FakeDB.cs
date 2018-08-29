using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public static class FakeDB
    {
        public static IEnumerable<Pet> petList = new List<Pet>(); 
        
        public static void InitData()
        {           
            var pet1 = new Pet()
            {
                ID = 1,
                Name = "Max",
                Type = "Dog",
                Birthdate = DateTime.Parse("10 / 05 / 1998"),
                SoldDate = DateTime.Parse("29 / 08 / 2018"),
                Color = "Black",
                PreviousOwner = "John Kowalski",
                Price = 19.99
            };

            var pet2 = new Pet()
            {
                ID = 2,
                Name = "Dog",
                Type = "Cat",
                Birthdate = DateTime.Parse("12 / 08 / 1410"),
                SoldDate = DateTime.Parse("28 / 11 / 1544"),
                Color = "Pink",
                PreviousOwner = "Fabio Monaco",
                Price = 105.50
            };

            var pet3 = new Pet()
            {
                ID = 3,
                Name = "Shrek",
                Type = "Ogre",
                Birthdate = DateTime.Parse("07 / 09 / 2001"),
                SoldDate = DateTime.Parse("08 / 07 / 2010"),
                Color = "Green",
                PreviousOwner = "Andrew Adamson",
                Price = 999.99
            };

            petList = new List<Pet>() { pet1, pet2, pet3 };
        }
    }
}
