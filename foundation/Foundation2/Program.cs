using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineOrdering
{
    // Address Class
    public class Address
    {
        // Private member variables
        private string streetAddress;
        private string city;
        private string stateOrProvince;
        private string country;

        // Constructor
        public Address(string streetAddress, string city, string stateOrProvince, string country)
        {
            this.streetAddress = streetAddress;
            this.city = city;
            this.stateOrProvince = stateOrProvince;
            this.country = country;
        }

        // Getters and Setters
        public string GetStreetAddress()
        {
            return streetAddress;
        }

        public void SetStreetAddress(string streetAddress)
        {
            this.streetAddress = streetAddress;
        }

        public string GetCity()
        {
            return city;
        }

        public void SetCity(string city)
        {
            this.city = city;
        }

        public string GetStateOrProvince()
        {
            return stateOrProvince;
        }

        public void SetStateOrProvince(string stateOrProvince)
        {
            this.stateOrProvince = stateOrProvince;
        }

        public string GetCountry()
        {
            return country;
        }

        public void SetCountry(string country)
        {
            this.country = country;
        }

        // Method to check if the address is in the USA
        public bool IsInUSA()
        {
            return country.Trim().ToUpper() == "USA";
        }

        // Method to return the full address as a string
        public override string ToString()
        {
            return $"{streetAddress}\n{city}, {stateOrProvince}\n{country}";
        }
    }

    // Customer Class
    public class Customer
    {
        // Private member variables
        private string name;
        private Address address;

        // Constructor
        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        // Getters and Setters
        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Address GetAddress()
        {
            return address;
        }

        public void SetAddress(Address address)
        {
            this.address = address;
        }

        // Method to check if the customer lives in the USA
        public bool IsInUSA()
        {
            return address.IsInUSA();
        }

        // Method to return the full customer information as a string
        public override string ToString()
        {
            return $"{name}\n{address.ToString()}";
        }
    }

    // Product Class
    public class Product
    {
        // Private member variables
        private string name;
        private string productId;
        private double price;
        private int quantity;

        // Constructor
        public Product(string name, string productId, double price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        // Getters and Setters
        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetProductId()
        {
            return productId;
        }

        public void SetProductId(string productId)
        {
            this.productId = productId;
        }

        public double GetPrice()
        {
            return price;
        }

        public void SetPrice(double price)
        {
            this.price = price;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        // Method to calculate total cost of the product
        public double GetTotalCost()
        {
            return price * quantity;
        }

        // Method to return packing label information
        public string GetPackingLabel()
        {
            return $"Product Name: {name}, Product ID: {productId}";
        }
    }

    // Order Class
    public class Order
    {
        // Private member variables
        private List<Product> products;
        private Customer customer;
        private const double USA_SHIPPING_COST = 5.0;
        private const double INTERNATIONAL_SHIPPING_COST = 35.0;

        // Constructor
        public Order(Customer customer)
        {
            this.customer = customer;
            this.products = new List<Product>();
        }

        // Method to add a product to the order
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        // Getters
        public List<Product> GetProducts()
        {
            return products;
        }

        public Customer GetCustomer()
        {
            return customer;
        }

        // Method to calculate total cost of the order
        public double GetTotalCost()
        {
            double total = 0.0;
            foreach (var product in products)
            {
                total += product.GetTotalCost();
            }

            // Add shipping cost
            if (customer.IsInUSA())
            {
                total += USA_SHIPPING_COST;
            }
            else
            {
                total += INTERNATIONAL_SHIPPING_COST;
            }

            return total;
        }

        // Method to generate packing label
        public string GetPackingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Packing Label:");
            foreach (var product in products)
            {
                sb.AppendLine(product.GetPackingLabel());
            }
            return sb.ToString();
        }

        // Method to generate shipping label
        public string GetShippingLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Shipping Label:");
            sb.AppendLine(customer.ToString());
            return sb.ToString();
        }
    }

    // Program Class
    class Program
    {
        static void Main(string[] args)
        {
            // Create Addresses
            Address address1 = new Address("123 Maple Street", "Springfield", "IL", "USA");
            Address address2 = new Address("456 Oak Avenue", "Toronto", "ON", "Canada");

            // Create Customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Jane Smith", address2);

            // Create Products for Order 1
            Product product1 = new Product("Widget", "W123", 10.99, 3);
            Product product2 = new Product("Gadget", "G456", 15.49, 2);

            // Create Order 1
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            // Create Products for Order 2
            Product product3 = new Product("Thingamajig", "T789", 7.25, 5);
            Product product4 = new Product("Doohickey", "D012", 12.75, 1);
            Product product5 = new Product("Whatsit", "W345", 5.50, 10);

            // Create Order 2
            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);
            order2.AddProduct(product5);

            // Display Order 1 Details
            Console.WriteLine("----- Order 1 -----");
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}\n");

            // Display Order 2 Details
            Console.WriteLine("----- Order 2 -----");
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}\n");

            // Keep the console window open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}