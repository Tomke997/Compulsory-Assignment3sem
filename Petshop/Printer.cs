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
                        Console.ReadLine();
                        break;

                    case 2:
                        PrintAnimalsByType();
                        Console.ReadLine();
                        break;
                    case 3:
                        Pet pet = CreatePet(-1);                      
                        _petService.AddPet(pet);
                        break;

                    case 4:
                        int petId = PrintAndReadID("ID: ", "Please insert a number");
                        if(_petService.FindPetById(petId) != null)
                        {
                            _petService.DeletePet(petId);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Pet with selected id does not exist");
                            Console.ReadLine();
                        }
                            break;

                    case 5:
                         petId = PrintAndReadID("ID: ", "Please insert a number");
                        if (_petService.FindPetById(petId) != null)
                        {
                             Console.Clear();
                             pet = CreatePet(petId);                           
                            _petService.UpdatePet(pet);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Pet with selected id does not exist");
                            Console.ReadLine();
                        }                           
                        break;

                    case 6:                       
                        List<Pet> petList; ;
                        petList =_petService.SortPetByPrice(_petService.GetPets());
                        PrintPetsInList(petList);
                        Console.ReadLine();
                        break;

                    case 7:
                        petList = _petService.SortPetByPrice(_petService.GetPets());
                        PrintPetsInList(_petService.GetSelectedAmountOfPets(petList, 5));
                        Console.ReadLine();
                        break;
                }               
                selection = PrintMenuItems(menuItems);
            }
        }

        Pet CreatePet(int id)
        {
            Pet pet = _petService.GetPetInstance();
            pet.ID = id;
            pet.Name = PrintAndRead("Name: ");
            pet.Type = PrintAndRead("Type: ");
            pet.Birthdate = PrintAndReadDateTime("Birthday: ", "Please insert a date");
            pet.SoldDate = PrintAndReadDateTime("SoldDate: ", "Please insert a date");
            pet.Color = PrintAndRead("Color: ");
           // pet.PreviousOwner = PrintAndRead("PreviousOwner: ");
            pet.Price = PrintAndReadPrice("Price: ", "Please insert a price");
            return pet;
        }

        void PrintAnimalsByType()
        {
            var type = PrintAndRead("Type: ");
            List<Pet> listOfPets = _petService.GetPetsByType(type);
            if (listOfPets == null)
            {
                Console.Clear();
                Console.WriteLine("No pets of the selected type");
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
            Console.WriteLine("Welcome to the best pet shop application \nselect wisely: \n");
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
                Console.Write("Please select a number between 1-8");
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
    }
}
