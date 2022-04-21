using System;
using System.Collections.Generic;

namespace DebugBasics
{
    enum Category
    {
        Electronics,
        Grocery,
        Clothes,
        Other
    }

    class Product
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }

        public Product(string name, Category category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }

    internal static class Utils
    {
        private const double VatGrocery = 0.07;
        private const double VatNormal = 0.19;

        public static double CalculateVat(double price, Category category)
        {
            double result;

            if (category == Category.Grocery)
                result = price * VatGrocery;
            else
                result = price * VatNormal;

            return result;
        }

        public static double CalculatePriceWithVat(double price, Category category)
        {
            var priceVat = price + CalculateVat(price, category);
            return priceVat;
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var products = new List<Product>
            {
                new Product("Batteries", Category.Electronics, 2.50),
                new Product("SD Card", Category.Electronics, 10),
                new Product("T-shirt", Category.Electronics, 15),
                new Product("", Category.Other, 20),
                new Product("Parmesan Cheese", Category.Grocery, 7.50),
                new Product("Tomatoes", Category.Grocery, 2),
            };
            
            ShowPricesWithVat(products);
        }

        private static void ShowPricesWithVat(IEnumerable<Product> products)
        {
            Console.WriteLine("Product prices incl. VAT:");

            foreach (var product in products)
            {
                var total = Math.Round(Utils.CalculatePriceWithVat(product.Price, product.Category), 2);
                PrintPrice(product.Name, total);
            }
        }

        private static void PrintPrice(string name, double price)
        {
            if (name == "")
                throw new ApplicationException("Product name cannot be empty");
            
            Console.WriteLine($"{name}: {price} EUR");
        }
    }
}