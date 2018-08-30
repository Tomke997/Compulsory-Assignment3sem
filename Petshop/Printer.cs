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
                   
                    case 2:
                        PrintPetsInList(_petService.GetPets());              
                        break;
                    case 4:
                        var name = PrintAndRead("Name: ");
                        var type = PrintAndRead("Type: ");
                        var birthday = PrintAndReadDateTime("Birthday: ");
                        var soldDate = PrintAndReadDateTime("SoldDate: ");
                        var color = PrintAndRead("Color: ");
                        var previousOwner = PrintAndRead("PreviousOwner: ");
                        var price = PrintAndReadPrice("Price: ");

                        Pet newPet =_petService.CreatePet(name, type, birthday, soldDate, color, previousOwner, price);
                        _petService.AddPet(newPet);
                        break;
                }
                Console.ReadLine();
                selection = PrintMenuItems(menuItems);
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
                Console.WriteLine($"ID: {pet.ID}, Name: {pet.Name}, Type: {pet.Type}, Birthdate: {pet.Birthdate}, SoldDate: {pet.SoldDate}, Color: {pet.Color}, PreviousOwner: {pet.PreviousOwner}, Price: {pet.Price}");
            }
        }

        string PrintAndRead(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        DateTime PrintAndReadDateTime(string text)
        {
            Console.Write(text);
            DateTime dateTime;
            while(!DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                PrintValidation("Insert correct date", text);
            }
            ClearValidation();
            return dateTime;
        }

        double PrintAndReadPrice(string text)
        {
            Console.Write(text);
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                PrintValidation("Insert correct price", text);           
            }
            ClearValidation();
            return price;
        }
        void PrintValidation(string alert,string text)
        {
            var cursorT = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorSize - 15);
            Console.Write(alert);
            Console.SetCursorPosition(0, cursorT - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(Console.CursorLeft, cursorT - 1);
            Console.Write(text);
        }

        void ClearValidation()
        {
            var cursorT = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorSize - 15);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(Console.CursorLeft, cursorT);
            
        }       
    }
}
