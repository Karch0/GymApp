using ConsoleApp4.Controllers;
using ConsoleApp4.Data;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Tests;

public class Tests
{
    private GymContext _context;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<GymContext>()
            .UseInMemoryDatabase("GymDataBase")
            .Options;

        _context = new GymContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
        [Test]
        public void AddClient_ShouldAddClient()
        {
            var controller = new ClientController(_context);
            var client = new Client
            {
                FullName = "Client One",
                Email = "client1@test.com",
                Points = 10
            };

            var added = controller.AddClient(client);

            Assert.That(added, Is.Not.Null);
            Assert.That(added.Id, Is.GreaterThan(0));
            Assert.That(_context.Clients.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetClientById_ShouldReturnCorrectClient()
        {
            var controller = new ClientController(_context);
            var client = new Client { FullName = "Client Test", Email = "test@test.com", Points = 5 };
            _context.Clients.Add(client);
            _context.SaveChanges();

            var result = controller.GetClientById(client.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FullName, Is.EqualTo("Client Test"));
            Assert.That(result.Email, Is.EqualTo("test@test.com"));
            Assert.That(result.Points, Is.EqualTo(5));
        }

        [Test]
        public void GetAllClients_ShouldReturnAllClients()
        {
            var controller = new ClientController(_context);
            var c1 = new Client { FullName = "Client1", Email = "c1@test.com", Points = 0 };
            var c2 = new Client { FullName = "Client2", Email = "c2@test.com", Points = 10 };
            _context.Clients.AddRange(c1, c2);
            _context.SaveChanges();

            var result = controller.GetAllClients();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(c => c.FullName == "Client1"), Is.True);
            Assert.That(result.Any(c => c.FullName == "Client2"), Is.True);
        }

        [Test]
        public void UpdateClient_ShouldModifyExistingClient()
        {
            var controller = new ClientController(_context);
            var client = new Client { FullName = "Old Name", Email = "old@test.com", Points = 0 };
            _context.Clients.Add(client);
            _context.SaveChanges();

            client.FullName = "New Name";
            client.Email = "new@test.com";
            client.Points = 15;

            var updated = controller.UpdateClient(client);

            Assert.That(updated, Is.True);

            var updatedClient = _context.Clients.Find(client.Id);
            Assert.That(updatedClient.FullName, Is.EqualTo("New Name"));
            Assert.That(updatedClient.Email, Is.EqualTo("new@test.com"));
            Assert.That(updatedClient.Points, Is.EqualTo(15));
        }

        [Test]
        public void DeleteClient_ShouldRemoveClient()
        {
            var controller = new ClientController(_context);
            var client = new Client { FullName = "ToDelete", Email = "delete@test.com", Points = 0 };
            _context.Clients.Add(client);
            _context.SaveChanges();

            var deleted = controller.DeleteClient(client.Id);

            Assert.That(deleted, Is.True);
            Assert.That(_context.Clients.Any(c => c.Id == client.Id), Is.False);
        }
        

        [Test]
        public void AddCoach_ShouldAddCoach()
        {
            var controller = new CoachController(_context);

            var gymLocation = new GymLocation { Name = "Gym1", Capacity = 50 };
            _context.GymLocations.Add(gymLocation);
            _context.SaveChanges();

            var coach = new Coach
            {
                FullName = "Coach One",
                Status = "Novice",
                ExperienceYears = 3,
                TrainingPrice = 25,
                GymLocationId = gymLocation.Id
            };

            var added = controller.AddCoach(coach);

            Assert.That(added, Is.Not.Null);
            Assert.That(added.Id, Is.GreaterThan(0));
            Assert.That(_context.Coaches.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetCoachById_ShouldReturnCorrectCoach()
        {
            var controller = new CoachController(_context);

            var gymLocation = new GymLocation { Name = "Gym2", Capacity = 60 };
            _context.GymLocations.Add(gymLocation);
            _context.SaveChanges();

            var coach = new Coach
            {
                FullName = "Coach Test",
                Status = "Expert",
                ExperienceYears = 15,
                TrainingPrice = 100,
                GymLocationId = gymLocation.Id
            };

            _context.Coaches.Add(coach);
            _context.SaveChanges();

            var result = controller.GetCoachById(coach.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FullName, Is.EqualTo("Coach Test"));
            Assert.That(result.GymLocationId, Is.EqualTo(gymLocation.Id));
        }

        [Test]
        public void GetAllCoaches_ShouldReturnAllCoaches()
        {
            var controller = new CoachController(_context);

            var gymLocation = new GymLocation { Name = "Gym3", Capacity = 40 };
            _context.GymLocations.Add(gymLocation);
            _context.SaveChanges();

            var c1 = new Coach { FullName = "Coach1", Status = "Novice", ExperienceYears = 1, TrainingPrice = 20, GymLocationId = gymLocation.Id };
            var c2 = new Coach { FullName = "Coach2", Status = "Advanced", ExperienceYears = 8, TrainingPrice = 60, GymLocationId = gymLocation.Id };
            _context.Coaches.AddRange(c1, c2);
            _context.SaveChanges();

            var result = controller.GetAllCoaches();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(c => c.FullName == "Coach1"), Is.True);
            Assert.That(result.Any(c => c.FullName == "Coach2"), Is.True);
        }

        [Test]
        public void UpdateCoach_ShouldModifyExistingCoach()
        {
            var controller = new CoachController(_context);

            var gymLocation = new GymLocation { Name = "Gym4", Capacity = 70 };
            _context.GymLocations.Add(gymLocation);
            _context.SaveChanges();

            var coach = new Coach
            {
                FullName = "Old Coach",
                Status = "Novice",
                ExperienceYears = 2,
                TrainingPrice = 30,
                GymLocationId = gymLocation.Id
            };

            _context.Coaches.Add(coach);
            _context.SaveChanges();

            coach.FullName = "New Coach";
            coach.Status = "Expert";
            coach.TrainingPrice = 50;

            var updated = controller.UpdateCoach(coach);

            Assert.That(updated, Is.True);

            var updatedCoach = _context.Coaches.Find(coach.Id);
            Assert.That(updatedCoach.FullName, Is.EqualTo("New Coach"));
            Assert.That(updatedCoach.Status, Is.EqualTo("Expert"));
            Assert.That(updatedCoach.TrainingPrice, Is.EqualTo(50));
        }

        [Test]
        public void DeleteCoach_ShouldRemoveCoach()
        {
            var controller = new CoachController(_context);

            var gymLocation = new GymLocation { Name = "Gym5", Capacity = 80 };
            _context.GymLocations.Add(gymLocation);
            _context.SaveChanges();

            var coach = new Coach
            {
                FullName = "Delete Coach",
                Status = "Novice",
                ExperienceYears = 2,
                TrainingPrice = 30,
                GymLocationId = gymLocation.Id
            };

            _context.Coaches.Add(coach);
            _context.SaveChanges();

            var deleted = controller.DeleteCoach(coach.Id);

            Assert.That(deleted, Is.True);
            Assert.That(_context.Coaches.Any(c => c.Id == coach.Id), Is.False);
        }
        

        [Test]
        public void AddGymLocation_ShouldAddLocation()
        {
            var controller = new GymLocationController(_context);

            var location = new GymLocation
            {
                Name = "Location One",
                Capacity = 100
            };

            var added = controller.AddGymLocation(location);

            Assert.That(added, Is.Not.Null);
            Assert.That(added.Id, Is.GreaterThan(0));
            Assert.That(_context.GymLocations.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetGymLocationById_ShouldReturnCorrectLocation()
        {
            var controller = new GymLocationController(_context);

            var location = new GymLocation { Name = "Location Test", Capacity = 50 };
            _context.GymLocations.Add(location);
            _context.SaveChanges();

            var result = controller.GetGymLocationById(location.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Location Test"));
            Assert.That(result.Capacity, Is.EqualTo(50));
        }

        [Test]
        public void GetAllGymLocations_ShouldReturnAllLocations()
        {
            var controller = new GymLocationController(_context);

            var loc1 = new GymLocation { Name = "Loc1", Capacity = 30 };
            var loc2 = new GymLocation { Name = "Loc2", Capacity = 60 };
            _context.GymLocations.AddRange(loc1, loc2);
            _context.SaveChanges();

            var result = controller.GetAllGymLocations();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(l => l.Name == "Loc1"), Is.True);
            Assert.That(result.Any(l => l.Name == "Loc2"), Is.True);
        }

        [Test]
        public void UpdateGymLocation_ShouldModifyExistingLocation()
        {
            var controller = new GymLocationController(_context);

            var location = new GymLocation { Name = "Old Loc", Capacity = 40 };
            _context.GymLocations.Add(location);
            _context.SaveChanges();

            location.Name = "New Loc";
            location.Capacity = 55;

            var updated = controller.UpdateGymLocation(location);

            Assert.That(updated, Is.True);

            var updatedLoc = _context.GymLocations.Find(location.Id);
            Assert.That(updatedLoc.Name, Is.EqualTo("New Loc"));
            Assert.That(updatedLoc.Capacity, Is.EqualTo(55));
        }

        [Test]
        public void DeleteGymLocation_ShouldRemoveLocation()
        {
            var controller = new GymLocationController(_context);

            var location = new GymLocation { Name = "Delete Loc", Capacity = 20 };
            _context.GymLocations.Add(location);
            _context.SaveChanges();

            var deleted = controller.DeleteGymLocation(location.Id);

            Assert.That(deleted, Is.True);
            Assert.That(_context.GymLocations.Any(l => l.Id == location.Id), Is.False);
        }
        

        [Test]
        public void AddSubscription_ShouldAddSubscription()
        {
            var controller = new SubscriptionController(_context);

            var gymLocation = new GymLocation { Name = "GymLoc1", Capacity = 50 };
            var client = new Client { FullName = "Sub Client", Email = "subclient@test.com", Points = 0 };
            _context.GymLocations.Add(gymLocation);
            _context.Clients.Add(client);
            _context.SaveChanges();

            var subscription = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                VisitationCounter = 0,
                IsInStreak = false,
                SubscriptionPrice = 100,
                GymLocationId = gymLocation.Id,
                ClientId = client.Id
            };

            var added = controller.AddSubscription(subscription);

            Assert.That(added, Is.Not.Null);
            Assert.That(added.Id, Is.GreaterThan(0));
            Assert.That(_context.Subscriptions.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetSubscriptionById_ShouldReturnCorrectSubscription()
        {
            var controller = new SubscriptionController(_context);

            var gymLocation = new GymLocation { Name = "GymLoc2", Capacity = 70 };
            var client = new Client { FullName = "ClientSub", Email = "clientsub@test.com", Points = 5 };
            _context.GymLocations.Add(gymLocation);
            _context.Clients.Add(client);
            _context.SaveChanges();

            var subscription = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2),
                VisitationCounter = 1,
                IsInStreak = true,
                SubscriptionPrice = 150,
                GymLocationId = gymLocation.Id,
                ClientId = client.Id
            };

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            var result = controller.GetSubscriptionById(subscription.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SubscriptionPrice, Is.EqualTo(150));
            Assert.That(result.IsInStreak, Is.True);
        }

        [Test]
        public void GetAllSubscriptions_ShouldReturnAllSubscriptions()
        {
            var controller = new SubscriptionController(_context);

            var gymLocation = new GymLocation { Name = "GymLoc3", Capacity = 40 };
            var client1 = new Client { FullName = "Client1", Email = "client1@test.com", Points = 0 };
            var client2 = new Client { FullName = "Client2", Email = "client2@test.com", Points = 10 };

            _context.GymLocations.Add(gymLocation);
            _context.Clients.AddRange(client1, client2);
            _context.SaveChanges();

            var s1 = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                VisitationCounter = 2,
                IsInStreak = false,
                SubscriptionPrice = 100,
                GymLocationId = gymLocation.Id,
                ClientId = client1.Id
            };

            var s2 = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                VisitationCounter = 0,
                IsInStreak = true,
                SubscriptionPrice = 200,
                GymLocationId = gymLocation.Id,
                ClientId = client2.Id
            };

            _context.Subscriptions.AddRange(s1, s2);
            _context.SaveChanges();

            var result = controller.GetAllSubscriptions();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(s => s.ClientId == client1.Id), Is.True);
            Assert.That(result.Any(s => s.ClientId == client2.Id), Is.True);
        }

        [Test]
        public void UpdateSubscription_ShouldModifyExistingSubscription()
        {
            var controller = new SubscriptionController(_context);

            var gymLocation = new GymLocation { Name = "GymLoc4", Capacity = 30 };
            var client = new Client { FullName = "ClientUpdate", Email = "update@test.com", Points = 0 };
            _context.GymLocations.Add(gymLocation);
            _context.Clients.Add(client);
            _context.SaveChanges();

            var subscription = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                VisitationCounter = 1,
                IsInStreak = false,
                SubscriptionPrice = 80,
                GymLocationId = gymLocation.Id,
                ClientId = client.Id
            };

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            subscription.VisitationCounter = 5;
            subscription.IsInStreak = true;
            subscription.SubscriptionPrice = 120;

            var updated = controller.UpdateSubscription(subscription);

            Assert.That(updated, Is.True);

            var updatedSub = _context.Subscriptions.Find(subscription.Id);
            Assert.That(updatedSub.VisitationCounter, Is.EqualTo(5));
            Assert.That(updatedSub.IsInStreak, Is.True);
            Assert.That(updatedSub.SubscriptionPrice, Is.EqualTo(120));
        }

        [Test]
        public void DeleteSubscription_ShouldRemoveSubscription()
        {
            var controller = new SubscriptionController(_context);

            var gymLocation = new GymLocation { Name = "GymLoc5", Capacity = 40 };
            var client = new Client { FullName = "ClientDel", Email = "del@test.com", Points = 0 };
            _context.GymLocations.Add(gymLocation);
            _context.Clients.Add(client);
            _context.SaveChanges();

            var subscription = new Subscription
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                VisitationCounter = 0,
                IsInStreak = false,
                SubscriptionPrice = 90,
                GymLocationId = gymLocation.Id,
                ClientId = client.Id
            };

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            var deleted = controller.DeleteSubscription(subscription.Id);

            Assert.That(deleted, Is.True);
            Assert.That(_context.Subscriptions.Any(s => s.Id == subscription.Id), Is.False);
        }
    }