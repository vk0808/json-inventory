using System;
using System.Collections.Generic;
using System.Text;

namespace JSONInventory
{
    class InventoryList
    {
        // Instance variable
        public string name;
        public double weight;
        public double pricePerkg;

        // Constructor
        public InventoryList(string name, double weight, double pricePerkg)
        {
            this.name = name;
            this.weight = weight;
            this.pricePerkg = pricePerkg;
        }
    }
}
