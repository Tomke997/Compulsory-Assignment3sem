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
                "Get 5 cheapest available Pets \n"
            };
            int selection = PrintMenuItems(menuItems);
            
        }

        int PrintMenuItems(List<string> menuItems)
        {
            Console.WriteLine("Welcome to the best petshop application \nselect wisely: \n");
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i+1}. {menuItems[i]}");
            }         
            int selection;
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out selection)
                || selection < 1
                || selection > 5)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Please select a number between 1-5");
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            return selection;
        }
    }
}
