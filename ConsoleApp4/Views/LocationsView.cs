using System;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models;
using ConsoleApp4.Models.Data;

namespace ConsoleApp4.Views
{
	public class LocationsView
	{
		private readonly GymContext _context;

		public LocationsView(GymContext context)
		{
			_context = context;
		}

		public void ShowMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Gym Locations Menu ---");
				Console.WriteLine("1. List all locations");
				Console.WriteLine("2. Add new location");
				Console.WriteLine("3. Delete location");
				Console.WriteLine("0. Exit");
				Console.Write("Select an option: ");
				var input = Console.ReadLine();

				switch (input)
				{
					case "1":
						ShowAllLocations();
						break;
					case "2":
						AddLocation();
						break;
					case "3":
						DeleteLocation();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		private void ShowAllLocations()
		{
			var locations = _context.GymLocations.ToList();
			Console.WriteLine("\nGym Locations:");
			foreach (var loc in locations)
			{
				Console.WriteLine($"ID: {loc.Id}, Name: {loc.Name}, Capacity: {loc.Capacity}");
			}
		}

		private void AddLocation()
		{
			Console.Write("Enter location name: ");
			var name = Console.ReadLine();

			Console.Write("Enter capacity: ");
			if (!int.TryParse(Console.ReadLine(), out int capacity))
			{
				Console.WriteLine("Invalid capacity.");
				return;
			}

			var location = new GymLocation
			{
				Name = name,
				Capacity = capacity
			};

			_context.GymLocations.Add(location);
			_context.SaveChanges();

			Console.WriteLine("Location added successfully.");
		}

		private void DeleteLocation()
		{
			Console.Write("Enter the ID of the location to delete: ");
			var input = Console.ReadLine();
			if (!int.TryParse(input, out int locationId))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var location = _context.GymLocations.FirstOrDefault(l => l.Id == locationId);
			if (location == null)
			{
				Console.WriteLine("Location not found.");
				return;
			}

			_context.GymLocations.Remove(location);
			_context.SaveChanges();
			Console.WriteLine("Location deleted successfully.");
		}
	}
}