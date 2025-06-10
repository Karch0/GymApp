using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4.Controllers
{
	public class GymLocationController
	{
		private readonly GymContext _context;

		public GymLocationController(GymContext context)
		{
			_context = context;
		}

		public GymLocationController() {}
		public GymLocation AddGymLocation(GymLocation gymLocation)
		{
			if (gymLocation == null) throw new ArgumentNullException(nameof(gymLocation));
			_context.GymLocations.Add(gymLocation);
			_context.SaveChanges();
			return gymLocation;
		}

		public GymLocation? GetGymLocationById(int id)
		{
			return _context.GymLocations
				.Include(g => g.Subscriptions)
				.Include(g => g.Coaches)
				.FirstOrDefault(g => g.Id == id);
		}

		public IEnumerable<GymLocation> GetAllGymLocations()
		{
			return _context.GymLocations
				.Include(g => g.Subscriptions)
				.Include(g => g.Coaches)
				.ToList();
		}

		public bool UpdateGymLocation(GymLocation updatedGymLocation)
		{
			if (updatedGymLocation == null) throw new ArgumentNullException(nameof(updatedGymLocation));
			var existingGymLocation = _context.GymLocations.FirstOrDefault(g => g.Id == updatedGymLocation.Id);
			if (existingGymLocation == null) return false;

			existingGymLocation.Name = updatedGymLocation.Name;
			existingGymLocation.Capacity = updatedGymLocation.Capacity;
			existingGymLocation.Subscriptions = updatedGymLocation.Subscriptions;
			existingGymLocation.Coaches = updatedGymLocation.Coaches;

			_context.SaveChanges();
			return true;
		}
		public bool DeleteGymLocation(int id)
		{
			var gymLocation = _context.GymLocations.FirstOrDefault(g => g.Id == id);
			if (gymLocation == null) return false;
			_context.GymLocations.Remove(gymLocation);
			_context.SaveChanges();
			return true;
		}
	}
}
