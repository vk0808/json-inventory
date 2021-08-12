using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
