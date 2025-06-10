using System;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models;
using ConsoleApp4.Models.Data;

namespace ConsoleApp4.Views
{
	public class CoachView
	{
		private readonly GymContext _context;

		public CoachView(GymContext context)
		{
			_context = context;
		}

		public void ShowMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Coach Menu ---");
				Console.WriteLine("1. List all coaches");
				Console.WriteLine("2. Add new coach");
				Console.WriteLine("3. Delete coach");
				Console.WriteLine("0. Exit");
				Console.Write("Select an option: ");
				var input = Console.ReadLine();

				switch (input)
				{
					case "1":
						ShowAllCoaches();
						break;
					case "2":
						AddCoach();
						break;
					case "3":
						DeleteCoach();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		private void ShowAllCoaches()
		{
			var coaches = _context.Coaches.ToList();
			Console.WriteLine("\nCoaches:");
			foreach (var coach in coaches)
			{
				Console.WriteLine($"ID: {coach.Id}, Name: {coach.FullName}, Status: {coach.Status}, Experience: {coach.ExperienceYears}, Training Price: {coach.TrainingPrice}");
			}
		}

		private void AddCoach()
		{
			Console.Write("Enter coach name: ");
			var name = Console.ReadLine();

			Console.Write("Enter coach status (Expert, Advanced, Intermediate, Novice): ");
			var status = Console.ReadLine();

			Console.Write("Enter coach experience years: ");
			int? experience = null;
			if (int.TryParse(Console.ReadLine(), out int expVal))
				experience = expVal;

			Console.Write("Enter training price: ");
			int trainingPrice = 0;
			int.TryParse(Console.ReadLine(), out trainingPrice);

			Console.Write("Enter gym location ID: ");
			int gymLocationId = 0;
			int.TryParse(Console.ReadLine(), out gymLocationId);

			var coach = new Coach
			{
				FullName = name,
				Status = status,
				ExperienceYears = experience,
				TrainingPrice = trainingPrice,
				GymLocationId = gymLocationId
			};

			_context.Coaches.Add(coach);
			_context.SaveChanges();

			Console.WriteLine("Coach added successfully.");
		}

		private void DeleteCoach()
		{
			Console.Write("Enter the ID of the coach to delete: ");
			var input = Console.ReadLine();
			if (!int.TryParse(input, out int coachId))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var coach = _context.Coaches.FirstOrDefault(c => c.Id == coachId);
			if (coach == null)
			{
				Console.WriteLine("Coach not found.");
				return;
			}

			_context.Coaches.Remove(coach);
			_context.SaveChanges();
			Console.WriteLine("Coach deleted successfully.");
		}
	}
}