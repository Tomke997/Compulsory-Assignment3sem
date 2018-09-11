using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public static class FakeDB
    {

        public static IEnumerable<Pet> petList;

        public static IEnumerable<Owner> ownerList;
        
        public static void InitData()
        {
            var owner1 = new Owner()
            {
                ID = 1,
                FirstName = "Fabio",
                LastName = "Monaco",
                Address = "WhiskeyStreet 997",
                PhoneNumber = "997111666"
            };

            var owner2 = new Owner()
            {
                ID = 2,
                FirstName = "John",
                LastName = "Kowalski",
                Address = "Zawiszy Czarnego 6",
                PhoneNumber = "797264765"
            };
            
            var pet1 = new Pet()
            {
                ID = 1,
                Name = "Max",
                Type = "Dog",
                Birthdate = DateTime.Parse("10 / 05 / 1998"),
                SoldDate = DateTime.Parse("29 / 08 / 2018"),
                Color = "Black",
                PreviousOwner = new Owner(){ ID = 1},
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
                PreviousOwner = new Owner(){ ID = 1},
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
                PreviousOwner = new Owner(){ ID = 2},
                Price = 99.99
            };

            petList = new List<Pet>() { pet1, pet2, pet3 }; 
            ownerList = new List<Owner>() {owner1, owner2};
        }
    }
}
