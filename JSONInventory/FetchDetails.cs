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
                Console.WriteLine("\nEnter the task you want to perform\n1. Add data into Inventory\n2. View Inventory details\n3. Exit\n");
                task = int.Parse(Console.ReadLine());

                if (task == 3) // exit
                {
                    Console.WriteLine("\nExiting...");
                    break;
                }

                if (task != 1 && task != 2) // when wrong number
                {
                    Console.WriteLine("You have enterd wrong task number\n");
                }
                else // performing task
                {
                    performTask(task);
                    task = 5; // to keep inside loop
                }
            }
        }

        // Display and return item - rice, wheat, pulses
        private string itemsMenu()
        {
            int itemChoice = 0;
            while (itemChoice != 1 && itemChoice != 2 && itemChoice != 3)
            {
                Console.WriteLine("\nEnter what you want add\n1. Rice\n2. Wheat\n3. Pulses\n");
                itemChoice = int.Parse(Console.ReadLine());

                if (itemChoice != 1 && itemChoice != 2 && itemChoice != 3)
                {
                    Console.WriteLine("You have enterd wrong number\n");
                }
            }
            return itemChoice == 1 ? "rice" : (itemChoice == 2 ? "wheat" : "pulses");
        }


        // Custom Method to get item details
        private double getDetails(string name, string info)
        {
            Console.WriteLine($"\nEnter {info} for {name}: ");
            double data = double.Parse(Console.ReadLine());
            return data;
        }

        // Method to write to json file
        private void writeJSON(string path, List<InventoryList> list)
        {
            // JSON-Serializing
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);

            Console.WriteLine("\nInventory details has been added successFully to JSON File.\n");
        }

        // Method to read json file
        private List<InventoryList> readJSON(string path)
        {
            //Deserializing JSON file
            if (File.Exists(path) == false)
            {
                List<InventoryList> list = new List<InventoryList>();
                writeJSON(path, list);
                Console.WriteLine("\nThere is no file. New json file created\n");
            }

            string file = File.ReadAllText(path);
            List<InventoryList> dataFile = JsonConvert.DeserializeObject<List<InventoryList>>(file);
            return dataFile;

        }


        // Method to perform task like add or view inventory details
        private void performTask(int choice)
        {
            switch (choice)
            {
                // Add to inventory
                case 1:

                    string confirm = "y";
                    while (confirm.ToLower() == "y")
                    {
                        bool notFound = true; ;
                        // create a list from the inventory json file
                        List<InventoryList> inventoryList = readJSON(@"..\..\..\InventoryList.json");

                        // display menu
                        string name = itemsMenu();

                        // loop through to every item in list
                        foreach (var invItem in inventoryList)
                        {
                            // check if the item present in list is equal to the name selected
                            // then update
                            if (invItem.name.Contains(name))
                            {
                                Console.WriteLine($"\nItem already exits. You can update\n1. Weight ({invItem.weight})\n2. Price per kg ({invItem.pricePerkg})");
                                int ch = int.Parse(Console.ReadLine());
                                switch (ch)
                                {
                                    case 1:
                                        Console.WriteLine($"\nEnter updated weight({invItem.name}): ");
                                        invItem.weight = double.Parse(Console.ReadLine());
                                        break;
                                    case 2:
                                        Console.WriteLine($"\nEnter updated price per kg({invItem.name}): ");
                                        invItem.pricePerkg = double.Parse(Console.ReadLine());
                                        break;
                                    default:
                                        break;
                                }
                                // use writeJSON() to write JSON
                                writeJSON(@"..\..\..\InventoryList.json", inventoryList);
                                notFound = false;
                                break;
                            }
                            else
                            {
                                notFound = true;
                            }

                        }

                        // if it's not there then add
                        if (notFound)
                        {
                            Console.WriteLine("\nItem doesnot exits.");
                            double weight = getDetails(name, "weight");
                            double pricePerKg = getDetails(name, "price per kg");
                            InventoryList item = new InventoryList(name, weight, pricePerKg);
                            inventoryList.Add(item);

                            // use writeJSON() to write JSON
                            writeJSON(@"..\..\..\InventoryList.json", inventoryList);

                            break;
                        }

                        // ask if they want to continue
                        Console.WriteLine("\nDo you want to add more: \ny: yes\nn: no\n");
                        confirm = Console.ReadLine();

                    }

                    break;


                // View inventory
                case 2:

                    Console.WriteLine("\nItems stored in Inventory:\n");


                    // use readJSON() to read JSON
                    List<InventoryList> dataFile = readJSON(@"..\..\..\InventoryList.json");

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
