using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4.Controllers
{
	public class SubscriptionController
	{
		private readonly GymContext _context;

		public SubscriptionController(GymContext context)
		{
			_context = context;
		}

		public SubscriptionController() {}

		public Subscription AddSubscription(Subscription subscription)
		{
			if (subscription == null) throw new ArgumentNullException(nameof(subscription));
			_context.Subscriptions.Add(subscription);
			_context.SaveChanges();
			return subscription;
		}

		public Subscription? GetSubscriptionById(int id)
		{
			return _context.Subscriptions
				.Include(s => s.GymLocation)
				.Include(s => s.Client)
				.FirstOrDefault(s => s.Id == id);
		}

		public IEnumerable<Subscription> GetAllSubscriptions()
		{
			return _context.Subscriptions
				.Include(s => s.GymLocation)
				.Include(s => s.Client)
				.ToList();
		}

		public bool UpdateSubscription(Subscription updatedSubscription)
		{
			if (updatedSubscription == null) throw new ArgumentNullException(nameof(updatedSubscription));
			var existingSubscription = _context.Subscriptions.FirstOrDefault(s => s.Id == updatedSubscription.Id);
			if (existingSubscription == null) return false;

			existingSubscription.StartDate = updatedSubscription.StartDate;
			existingSubscription.EndDate = updatedSubscription.EndDate;
			existingSubscription.VisitationCounter = updatedSubscription.VisitationCounter;
			existingSubscription.IsInStreak = updatedSubscription.IsInStreak;
			existingSubscription.SubscriptionPrice = updatedSubscription.SubscriptionPrice;
			existingSubscription.GymLocationId = updatedSubscription.GymLocationId;
			existingSubscription.GymLocation = updatedSubscription.GymLocation;
			existingSubscription.ClientId = updatedSubscription.ClientId;
			existingSubscription.Client = updatedSubscription.Client;

			_context.SaveChanges();
			return true;
		}
		public bool DeleteSubscription(int id)
		{
			var subscription = _context.Subscriptions.FirstOrDefault(s => s.Id == id);
			if (subscription == null) return false;
			_context.Subscriptions.Remove(subscription);
			_context.SaveChanges();
			return true;
		}
	}
}
