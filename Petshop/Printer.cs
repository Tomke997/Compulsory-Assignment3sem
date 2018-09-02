using Petshop.Core.ApplicationService;
using Petshop.Core.Entity;
using System.Collections.Generic;
using System;
using System.ComponentModel;

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
                        //CHANGE!!
                        var type = PrintAndRead("Type: ");
                        List<Pet> listOfPets = _petService.GetPetsByType(type);
                        if(listOfPets==null)
                        {
                            Console.Clear();
                            Console.WriteLine("No animals of the selected type");
                        }
                        else
                        {
                            Console.Clear();
                            PrintPetsInList(listOfPets);
                        }
                        break;
                    case 3:
                        var name = PrintAndRead("Name: ");
                        type = PrintAndRead("Type: ");
                        var birthday = PrintAndReadDateTime("Birthday: ", "Insert correct date");
                        var soldDate = PrintAndReadDateTime("SoldDate: ", "Insert correct date");
                        var color = PrintAndRead("Color: ");
                        var previousOwner = PrintAndRead("PreviousOwner: ");
                        var price = PrintAndReadPrice("Price: ","Insert correct price");

                        Pet newPet =_petService.CreatePet(name, type, birthday, soldDate, color, previousOwner, price);
                        _petService.AddPet(newPet);
                        break;
                    case 4:
                        int foundPetIdDelete;
                        if (int.TryParse(PrintAndRead("Id: "), out foundPetIdDelete))
                        {
                            _petService.DeletePet(foundPetIdDelete);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("insert number");
                        }
                            break;
                    case 5:
                        //CHANGE IT TEST
                         int foundPetId;
                        if (int.TryParse(PrintAndRead("Id: "), out foundPetId))
                        {
                            if (_petService.FindPetById(foundPetId) == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Pet with this Id does not exist");
                            }
                            else
                            {
                                Console.Clear();
                                 name = PrintAndRead("Name: ");
                                type = PrintAndRead("Type: ");
                                 birthday = PrintAndReadDateTime("Birthday: ", "Insert correct date");
                                 soldDate = PrintAndReadDateTime("SoldDate: ", "Insert correct date");
                                 color = PrintAndRead("Color: ");
                                 previousOwner = PrintAndRead("PreviousOwner: ");
                                price = PrintAndReadPrice("Price: ", "Insert correct price");
                                Pet updatedPet = new Pet()
                                {
                                    ID=foundPetId,
                                    Name = name,
                                    Birthdate=birthday,
                                    SoldDate=soldDate,
                                    Color=color,
                                    PreviousOwner=previousOwner,
                                   Price=price
                                };
                                _petService.UpdatePet(updatedPet);
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Isert Number");
                        }
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
                Console.WriteLine($"ID: {pet.ID}, Name: {pet.Name}, Type: {pet.Type}, Birthdate: {pet.Birthdate.ToString("dd/MM/yyyy")}, SoldDate: {pet.SoldDate.ToString("dd/MM/yyyy")}, Color: {pet.Color}, PreviousOwner: {pet.PreviousOwner}, Price: {pet.Price}");
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
        
    }
}
