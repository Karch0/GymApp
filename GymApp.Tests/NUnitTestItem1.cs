using ConsoleApp4.Controllers;
using ConsoleApp4.Data;
using ConsoleApp4.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace GymApp.Tests
{
    public class ShopControllerTests
    {
        private GymContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GymContext>()
                .UseInMemoryDatabase("ShopTestDb")
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
        public void AddShop_ShouldAddShop()
        {
            var controller = new ShopController(_context);
            var shop = new RewardsShop("Protein Bar", "Tasty snack", 50);

            var added = controller.AddShop(shop);

            Assert.That(added, Is.Not.Null);
            Assert.That(added.ID, Is.GreaterThan(0));
            Assert.That(_context.RewardsShops.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetShopById_ShouldReturnCorrectShop()
        {
            var controller = new ShopController(_context);
            var shop = new RewardsShop("Water Bottle", "Reusable", 30);
            _context.RewardsShops.Add(shop);
            _context.SaveChanges();

            var result = controller.GetShopById(shop.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Water Bottle"));
            Assert.That(result.Description, Is.EqualTo("Reusable"));
            Assert.That(result.Price, Is.EqualTo(30));
        }

        [Test]
        public void GetAllShops_ShouldReturnAllShops()
        {
            var controller = new ShopController(_context);
            var s1 = new RewardsShop("Shaker", "Mixes well", 20);
            var s2 = new RewardsShop("Towel", "Soft", 15);
            _context.RewardsShops.AddRange(s1, s2);
            _context.SaveChanges();

            var result = controller.GetAllShops();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(s => s.Name == "Shaker"), Is.True);
            Assert.That(result.Any(s => s.Name == "Towel"), Is.True);
        }

        [Test]
        public void UpdateShop_ShouldModifyExistingShop()
        {
            var controller = new ShopController(_context);
            var shop = new RewardsShop("Bag", "Small", 40);
            _context.RewardsShops.Add(shop);
            _context.SaveChanges();

            shop.Name = "Large Bag";
            shop.Description = "Spacious";
            shop.Price = 60;

            var updated = controller.UpdateShop(shop);

            Assert.That(updated, Is.True);

            var updatedShop = _context.RewardsShops.Find(shop.ID);
            Assert.That(updatedShop.Name, Is.EqualTo("Large Bag"));
            Assert.That(updatedShop.Description, Is.EqualTo("Spacious"));
            Assert.That(updatedShop.Price, Is.EqualTo(60));
        }

        [Test]
        public void DeleteShop_ShouldRemoveShop()
        {
            var controller = new ShopController(_context);
            var shop = new RewardsShop("Headband", "Sweat absorbent", 10);
            _context.RewardsShops.Add(shop);
            _context.SaveChanges();

            var deleted = controller.DeleteShop(shop.ID);

            Assert.That(deleted, Is.True);
            Assert.That(_context.RewardsShops.Any(s => s.ID == shop.ID), Is.False);
        }
    }
}
