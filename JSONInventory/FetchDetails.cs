﻿using System;
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
        public int menu()
        {
            int task = 0;
            while (task != 1 && task != 2)
            {
                Console.WriteLine("Enter the task you want to perform\n1. Add data into Inventory\n2. View Inventory details");
                task = int.Parse(Console.ReadLine());

                if (task != 1 && task != 2)
                {
                    Console.WriteLine("You have enterd wrong task number\n");
                }
            }
            return task;
        }


        public string itemsMenu()
        {
            int itemChoice = 0;
            while (itemChoice != 1 && itemChoice != 2 && itemChoice != 3)
            {
                Console.WriteLine("Enter what you want add\n1. Rice\n2. Wheat\n3. Pulses");
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
            Console.WriteLine($"Enter {info} for {name}: ");
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

                        Console.WriteLine("Item added to list. Do you want to add more: \ny: yes\nn: no");
                        confirm = Console.ReadLine();
                    }

                    // JSON-Serializing
                    string json = JsonConvert.SerializeObject(inventoryList);
                    File.WriteAllText(@"..\..\..\InventoryList.json", json);

                    Console.WriteLine("Inventory details has been added successFully to JSON File.");
                    break;


                // View inventory
                case 2:
                    break;

                default:
                    break;
            }
        }
    }
}
