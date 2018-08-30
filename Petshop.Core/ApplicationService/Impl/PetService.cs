using System;
using System.Collections.Generic;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using System.Linq;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        readonly IPetRepository _perRepository;
        public PetService(IPetRepository petRepository)
        {
            _perRepository = petRepository;
        }

        public Pet AddPet(Pet newPet)
        {
            _perRepository.AddPet(newPet);
            return newPet;
        }

        public Pet CreatePet(string Name, string Type, DateTime Birthdate, DateTime SoldDate, string Color, string PreviousOwner, double Price)
        {
            return new Pet()
            {
                Name = Name,
                Type = Type,
                Birthdate = Birthdate,
                SoldDate = SoldDate,
                Color = Color,
                PreviousOwner = PreviousOwner,
                Price = Price
            };
        }

        public List<Pet> GetPets()
        {
            return _perRepository.ReadPets().ToList();
        }

        public void PrintValidation(string alert, string text)
        {
            var cursorT = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorSize - 15);
            Console.Write(alert);
            Console.SetCursorPosition(0, cursorT - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(Console.CursorLeft, cursorT - 1);
            Console.Write(text);
        }

        public void ClearValidation()
        {
            var cursorT = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorSize - 15);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(Console.CursorLeft, cursorT);

        }

        public List<Pet> GetPetsByType(string type)
        {
            List<Pet> listWithType = new List<Pet>();
            foreach (Pet pet in _perRepository.ReadPets().ToList())
            {
                if (pet.Type.ToUpper() == type.ToUpper())
                {
                    listWithType.Add(pet);
                }               
            }
            if(listWithType.Count==0)
            {
                return null;
            }
            return listWithType;
        }

        public Pet DeletePet(int selectedId)
        {
           return _perRepository.RemovePet(selectedId);
        }
    }
}
