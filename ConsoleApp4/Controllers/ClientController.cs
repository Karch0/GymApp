using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp4.Data;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4.Controllers
{
	public class ClientController
	{

		private readonly GymContext _context;
		private int _nextId = 1;

		public ClientController(GymContext context)
		{
			_context = context;
		}

		public ClientController() {}

		public Client AddClient(Client client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			client.Id = _nextId++;
			_context.Clients.Add(client);
			_context.SaveChanges();
			return client;
		}

		public Client? GetClientById(int id)
		{
			return _context.Clients.FirstOrDefault(c => c.Id == id);
		}

		public IEnumerable<Client> GetAllClients()
		{
			return _context.Clients;
		}

		public bool UpdateClient(Client updatedClient)
		{
			if (updatedClient == null) throw new ArgumentNullException(nameof(updatedClient));
			var existingClient = _context.Clients.FirstOrDefault(c => c.Id == updatedClient.Id);
			if (existingClient == null) return false;

			existingClient.FullName = updatedClient.FullName;
			existingClient.Points = updatedClient.Points;
			existingClient.Email = updatedClient.Email;
			existingClient.CoachId = updatedClient.CoachId;
			existingClient.Coach = updatedClient.Coach;
			existingClient.Subscription = updatedClient.Subscription;

			return true;
		}
		public bool DeleteClient(int id)
		{
			var client = _context.Clients.FirstOrDefault(c => c.Id == id);
			if (client == null) return false;
			_context.Clients.Remove(client);
			_context.SaveChanges();
			return true;
		}
	}
}
