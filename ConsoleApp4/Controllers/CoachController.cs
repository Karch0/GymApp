using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4.Controllers
{
	public class CoachController
	{
		private readonly GymContext _context;

		public CoachController(GymContext context)
		{
			_context = context;
		}

		public CoachController() {}

		public Coach AddCoach(Coach coach)
		{
			if (coach == null) throw new ArgumentNullException(nameof(coach));
			_context.Coaches.Add(coach);
			_context.SaveChanges();
			return coach;
		}

		public Coach? GetCoachById(int id)
		{
			return _context.Coaches
				.Include(c => c.GymLocation)
				.Include(c => c.Clients)
				.FirstOrDefault(c => c.Id == id);
		}

		public IEnumerable<Coach> GetAllCoaches()
		{
			return _context.Coaches
				.Include(c => c.GymLocation)
				.Include(c => c.Clients)
				.ToList();
		}

		public bool UpdateCoach(Coach updatedCoach)
		{
			if (updatedCoach == null) throw new ArgumentNullException(nameof(updatedCoach));
			var existingCoach = _context.Coaches.FirstOrDefault(c => c.Id == updatedCoach.Id);
			if (existingCoach == null) return false;

			existingCoach.FullName = updatedCoach.FullName;
			existingCoach.Status = updatedCoach.Status;
			existingCoach.TrainingPrice = updatedCoach.TrainingPrice;
			existingCoach.GymLocationId = updatedCoach.GymLocationId;
			existingCoach.GymLocation = updatedCoach.GymLocation;
			existingCoach.Clients = updatedCoach.Clients;

			_context.SaveChanges();
			return true;
		}
		public bool DeleteCoach(int id)
		{
			var coach = _context.Coaches.FirstOrDefault(c => c.Id == id);
			if (coach == null) return false;
			_context.Coaches.Remove(coach);
			_context.SaveChanges();
			return true;
		}
	}
}
