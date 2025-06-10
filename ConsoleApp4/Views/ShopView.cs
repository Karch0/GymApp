using ConsoleApp4.Controllers;
using ConsoleApp4.Data.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp4.Views
{
    public class ShopView
    {
        private readonly ShopController _controller;

        public ShopView(ShopController controller)
        {
            _controller = controller;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Rewards Shop Menu ---");
                Console.WriteLine("1. List all shop items");
                Console.WriteLine("2. Add new shop item");
                Console.WriteLine("3. Update shop item");
                Console.WriteLine("4. Delete shop item");
                Console.WriteLine("5. View shop item by ID");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ListAll();
                        break;
                    case "2":
                        Add();
                        break;
                    case "3":
                        Update();
                        break;
                    case "4":
                        Delete();
                        break;
                    case "5":
                        ViewById();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private void ListAll()
        {
            var items = _controller.GetAllShops();
            Console.WriteLine("\n--- Shop Items ---");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ID}, Name: {item.Name}, Description: {item.Description}, Price: {item.Price}");
            }
        }

        private void Add()
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();
            Console.Write("Enter description: ");
            var description = Console.ReadLine();
            Console.Write("Enter price: ");
            if (!int.TryParse(Console.ReadLine(), out int price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            var shop = new RewardsShop(name, description, price);
            _controller.AddShop(shop);
            Console.WriteLine("Shop item added.");
        }

        private void Update()
        {
            Console.Write("Enter ID of item to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var shop = _controller.GetShopById(id);
            if (shop == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.Write("Enter new name: ");
            shop.Name = Console.ReadLine();
            Console.Write("Enter new description: ");
            shop.Description = Console.ReadLine();
            Console.Write("Enter new price: ");
            if (!int.TryParse(Console.ReadLine(), out int price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }
            shop.Price = price;

            if (_controller.UpdateShop(shop))
                Console.WriteLine("Shop item updated.");
            else
                Console.WriteLine("Update failed.");
        }

        private void Delete()
        {
            Console.Write("Enter ID of item to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            if (_controller.DeleteShop(id))
                Console.WriteLine("Shop item deleted.");
            else
                Console.WriteLine("Delete failed. Item not found.");
        }

        private void ViewById()
        {
            Console.Write("Enter ID of item to view: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var shop = _controller.GetShopById(id);
            if (shop == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine($"ID: {shop.ID}, Name: {shop.Name}, Description: {shop.Description}, Price: {shop.Price}");
        }
    }
}

