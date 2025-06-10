using System;
using System.Linq;
using ConsoleApp4.Data;   
using ConsoleApp4.Models;
using ConsoleApp4.Models.Data; 

namespace ConsoleApp4.Views
{
	public class ClientView
	{
		private readonly GymContext _context;

		public ClientView(GymContext context)
		{
			_context = context;
		}

		public void ShowMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Client Menu ---");
				Console.WriteLine("1. List all clients");
				Console.WriteLine("2. Add new client");
				Console.WriteLine("0. Exit");
				Console.Write("Select an option: ");
				var input = Console.ReadLine();

				switch (input)
				{
					case "1":
						ShowAllClients();
						break;
					case "2":
						AddClient();
						break;
					case "3":
						DeleteClient();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		private void ShowAllClients()
		{
			var clients = _context.Clients.ToList();
			Console.WriteLine("\nClients:");
			foreach (var client in clients)
			{
				Console.WriteLine($"ID: {client.Id}, Name: {client.FullName}, Email: {client.Email}");
			}
		}

		private void AddClient()
		{
			Console.Write("Enter client name: ");
			var name = Console.ReadLine();

			Console.Write("Enter client email: ");
			var email = Console.ReadLine();

			var client = new Client
			{
				FullName = name,
				Email = email
			};

			_context.Clients.Add(client);
			_context.SaveChanges();

			Console.WriteLine("Client added successfully.");
		}
		private void DeleteClient()
		{
			Console.Write("Enter the ID of the client to delete: ");
			if (int.TryParse(Console.ReadLine(), out int clientId))
			{
				var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
				if (client != null)
				{
					_context.Clients.Remove(client);
					_context.SaveChanges();
					Console.WriteLine("Client deleted successfully.");
				} else
				{
					Console.WriteLine("Client not found.");
				}
			} else
			{
				Console.WriteLine("Invalid ID.");
			}
		}
	}
}