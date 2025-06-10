using System;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models;
using ConsoleApp4.Models.Data;

namespace ConsoleApp4.Views
{
	public class SubscriptionView
	{
		private readonly GymContext _context;

		public SubscriptionView(GymContext context)
		{
			_context = context;
		}

		public void ShowMenu()
		{
			while (true)
			{
				Console.WriteLine("\n--- Subscription Menu ---");
				Console.WriteLine("1. List all subscriptions");
				Console.WriteLine("2. Add new subscription");
				Console.WriteLine("3. Delete subscription");
				Console.WriteLine("0. Exit");
				Console.Write("Select an option: ");
				var input = Console.ReadLine();

				switch (input)
				{
					case "1":
						ShowAllSubscriptions();
						break;
					case "2":
						AddSubscription();
						break;
					case "3":
						DeleteSubscription();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid option. Please try again.");
						break;
				}
			}
		}

		private void ShowAllSubscriptions()
		{
			var subscriptions = _context.Subscriptions.ToList();
			Console.WriteLine("\nSubscriptions:");
			foreach (var sub in subscriptions)
			{
				Console.WriteLine($"ID: {sub.Id}, Start: {sub.StartDate:yyyy-MM-dd}, End: {sub.EndDate:yyyy-MM-dd}, " +
								  $"Visits: {sub.VisitationCounter}, Price: {sub.SubscriptionPrice}, " +
								  $"ClientID: {sub.ClientId}, GymLocationID: {sub.GymLocationId}");
			}
		}

		private void AddSubscription()
		{
			Console.Write("Enter start date (yyyy-MM-dd): ");
			if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
			{
				Console.WriteLine("Invalid date format.");
				return;
			}

			Console.Write("Enter end date (yyyy-MM-dd): ");
			if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
			{
				Console.WriteLine("Invalid date format.");
				return;
			}

			Console.Write("Enter visitation counter: ");
			if (!int.TryParse(Console.ReadLine(), out int visitationCounter))
			{
				Console.WriteLine("Invalid number.");
				return;
			}

			Console.Write("Is in streak? (y/n): ");
			var isInStreakInput = Console.ReadLine();
			bool isInStreak = isInStreakInput?.Trim().ToLower() == "y";

			Console.Write("Enter streak week counter: ");
			int streakWeekCounter = 0;
			int.TryParse(Console.ReadLine(), out streakWeekCounter);

			Console.Write("Enter subscription price: ");
			if (!int.TryParse(Console.ReadLine(), out int subscriptionPrice))
			{
				Console.WriteLine("Invalid price.");
				return;
			}

			Console.Write("Enter gym location ID: ");
			if (!int.TryParse(Console.ReadLine(), out int gymLocationId))
			{
				Console.WriteLine("Invalid gym location ID.");
				return;
			}

			Console.Write("Enter client ID: ");
			if (!int.TryParse(Console.ReadLine(), out int clientId))
			{
				Console.WriteLine("Invalid client ID.");
				return;
			}

			var subscription = new Subscription
			{
				StartDate = startDate,
				EndDate = endDate,
				VisitationCounter = visitationCounter,
				IsInStreak = isInStreak,
				StreakWeekCounter = streakWeekCounter,
				SubscriptionPrice = subscriptionPrice,
				GymLocationId = gymLocationId,
				ClientId = clientId
			};

			_context.Subscriptions.Add(subscription);
			_context.SaveChanges();

			Console.WriteLine("Subscription added successfully.");
		}

		private void DeleteSubscription()
		{
			Console.Write("Enter the ID of the subscription to delete: ");
			var input = Console.ReadLine();
			if (!int.TryParse(input, out int subscriptionId))
			{
				Console.WriteLine("Invalid ID format.");
				return;
			}

			var subscription = _context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
			if (subscription == null)
			{
				Console.WriteLine("Subscription not found.");
				return;
			}

			_context.Subscriptions.Remove(subscription);
			_context.SaveChanges();
			Console.WriteLine("Subscription deleted successfully.");
		}
	}
}