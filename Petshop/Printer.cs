using Petshop.Core.ApplicationService;
using Petshop.Core.Entity;
using System.Collections.Generic;
using System;

namespace Petshop
{
    public class Printer : IPrinter
    {
        readonly IPetService _petService;
        public Printer(IPetService petService)
        {
            _petService = petService;
        }

        public void PrintMenu()
        {
            List<string> menuItems = new List<string>()
            {
                "Show list of all Pets",
                "Search Pets by Type",
                "Create a new Pet",
                "Delete Pet",
                "Update a Pet",
                "Sort Pets By Price",
                "Get 5 cheapest available Pets",
                "Exit \n"
            };
            int selection = PrintMenuItems(menuItems);
            while (selection != 8)
            {
                Console.Clear();
                switch (selection)
                {
                   
                    case 1:
                        PrintPetsInList(_petService.GetPets());              
                        break;

                    case 2:

                        PrintAnimalsByType();
                        break;
                    case 3:
                        Pet pet = CreatePet(-1);
                       // Pet newPet =_petService.CreatePet(name, type, birthday, soldDate, color, previousOwner, price);
                        _petService.AddPet(pet);
                        break;

                    case 4:
                        int petId = PrintAndReadID("ID: ", "insert number");
                        if(_petService.FindPetById(petId) != null)
                        {
                            _petService.DeletePet(petId);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Pet with this Id does not exist");
                        }
                            break;

                    case 5:
                         petId = PrintAndReadID("ID: ", "insert number");
                        if (_petService.FindPetById(petId) != null)
                        {
                             Console.Clear();
                             pet = CreatePet(petId);                           
                            _petService.UpdatePet(pet);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Pet with this Id does not exist");
                        }                           
                        break;

                    case 6:                       
                        List<Pet> petList; ;
                        petList =_petService.SortPetByPrice(_petService.GetPets());
                        PrintPetsInList(petList);
                        break;

                    case 7:
                        petList = _petService.SortPetByPrice(_petService.GetPets());
                        PrintPetsInList(_petService.GetSelectedAmountOfPets(petList, 5));
                        break;
                }
                Console.ReadLine();
                selection = PrintMenuItems(menuItems);
            }
        }

        Pet CreatePet(int id)
        {
            var name = PrintAndRead("Name: ");
            var type = PrintAndRead("Type: ");
            var birthday = PrintAndReadDateTime("Birthday: ", "Insert correct date");
            var soldDate = PrintAndReadDateTime("SoldDate: ", "Insert correct date");
            var color = PrintAndRead("Color: ");
            var previousOwner = PrintAndRead("PreviousOwner: ");
            var price = PrintAndReadPrice("Price: ", "Insert correct price");
            return new Pet()
            {
                ID = id,
                Name = name,
                Type = type,
                Birthdate = birthday,
                SoldDate = soldDate,
                Color = color,
                PreviousOwner = previousOwner,
                Price = price
            };

        }

        void PrintAnimalsByType()
        {
            var type = PrintAndRead("Type: ");
            List<Pet> listOfPets = _petService.GetPetsByType(type);
            if (listOfPets == null)
            {
                Console.Clear();
                Console.WriteLine("No animals of the selected type");
            }
            else
            {
                Console.Clear();
                PrintPetsInList(listOfPets);
            }
        }

        int PrintMenuItems(List<string> menuItems)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the best petshop application \nselect wisely: \n");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i+1}. {menuItems[i]}");
            }         
            int selection;
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out selection)
                || selection < 1
                || selection > 8)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Please select a number between 1-5");
                Console.SetCursorPosition(0, Console.CursorTop);
            }           
            return selection;
        }

        void PrintPetsInList(List<Pet> petList)
        {
            foreach (Pet pet in petList)
            {
                Console.WriteLine($"ID: {pet.ID}, Name: {pet.Name}, Type: {pet.Type}, Birthdate: {pet.Birthdate.ToString("dd/MM/yyyy")}, SoldDate: {pet.SoldDate.ToString("dd/MM/yyyy")}, Color: {pet.Color}, PreviousOwner: {pet.PreviousOwner}, Price: {pet.Price}\n");
            }
        }

        string PrintAndRead(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        #region Validations
        DateTime PrintAndReadDateTime(string text,string information)
        {
            Console.Write(text);
            DateTime dateTime;
            while(!DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                _petService.PrintValidation(information, text);
            }
            _petService.ClearValidation();
            return dateTime;
        }

        double PrintAndReadPrice(string text,string information)
        {          
            Console.Write(text);
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                _petService.PrintValidation(information, text);           
            }
            _petService.ClearValidation();
            return price;
        }

        int PrintAndReadID(string text, string information)
        {
            Console.Write(text);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                _petService.PrintValidation(information, text);
            }
            _petService.ClearValidation();
            return id;
        }
        #endregion

    }
}
