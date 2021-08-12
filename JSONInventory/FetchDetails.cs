using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization.Json;

namespace JSONInventory
{
    public class FetchDetails
    {
        // Method to display welcome message
        public string welcome()
        {
            return "Welcome to JSON Inventory Data Management System\n";
        }

        // Method to display menu and get task 
        public void menu()
        {
            int task = 5;
            while (task != 1 && task != 2 && task != 3)
            {
                Console.WriteLine("Enter the task you want to perform\n1. Add data into Inventory\n2. View Inventory details\n3. Exit");
                task = int.Parse(Console.ReadLine());

                if (task == 3)
                {
                    Console.WriteLine("\nExiting...");
                    break;
                }

                if (task != 1 && task != 2)
                {
                    Console.WriteLine("You have enterd wrong task number\n");
                }
                else
                {
                    performTask(task);
                    task = 5;
                }
            }
        }


        public string itemsMenu()
        {
            int itemChoice = 0;
            while (itemChoice != 1 && itemChoice != 2 && itemChoice != 3)
            {
                Console.WriteLine("\nEnter what you want add\n1. Rice\n2. Wheat\n3. Pulses");
                itemChoice = int.Parse(Console.ReadLine());

                if (itemChoice != 1 && itemChoice != 2 && itemChoice != 3)
                {
                    Console.WriteLine("You have enterd wrong number\n");
                }
            }
            return itemChoice == 1 ? "rice" : (itemChoice == 2 ? "wheat" : "pulses");
        }


        // Custom Method to get item details
        public double getDetails(string name, string info)
        {
            Console.WriteLine($"\nEnter {info} for {name}: ");
            double data = double.Parse(Console.ReadLine());
            return data;
        }


        // Method to perform task like add or view inventory details
        public void performTask(int choice)
        {
            switch (choice)
            {
                // Add to inventory
                case 1:
                    List<InventoryList> inventoryList = new List<InventoryList>();
                    string confirm = "y";
                    while (confirm.ToLower() == "y")
                    {

                        string name = itemsMenu();
                        double weight = getDetails(name, "weight");
                        double pricePerKg = getDetails(name, "price per kg");

                        InventoryList item = new InventoryList(name, weight, pricePerKg);

                        inventoryList.Add(item);

                        Console.WriteLine("\nItem added to list. Do you want to add more: \ny: yes\nn: no");
                        confirm = Console.ReadLine();
                    }

                    // JSON-Serializing
                    string json = JsonConvert.SerializeObject(inventoryList);
                    File.WriteAllText(@"..\..\..\InventoryList.json", json);

                    Console.WriteLine("\nInventory details has been added successFully to JSON File.");

                    break;


                // View inventory
                case 2:

                    Console.WriteLine("\nItems stored in Inventory:\n");

                    //Deserializing JSON file
                    string file = File.ReadAllText(@"..\..\..\InventoryList.json");
                    List<InventoryList> dataFile = JsonConvert.DeserializeObject<List<InventoryList>>(file);

                    //Display data stored in JSON file
                    foreach (var item in dataFile)
                    {
                        // Display name, weight, price per kg
                        Console.WriteLine($"Name : {item.name}\nWeight : {item.weight}\nPrice per Kg: {item.pricePerkg}");

                        // Calulate the total value
                        Console.WriteLine($"Total value of {item.name} =  {(item.weight * item.pricePerkg)}: ");
                        Console.WriteLine("--------------------------------------------\n");
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
