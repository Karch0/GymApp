using ConsoleApp4.Data;
using ConsoleApp4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Controllers
{
    public class ShopController
    {
        private readonly GymContext _context;
        public ShopController(GymContext context)
        {
            _context = context;
        }

        public ShopController() { }

        public RewardsShop AddShop(RewardsShop shop)
        {
            if (shop == null) throw new ArgumentNullException(nameof(shop));
            _context.RewardsShops.Add(shop);
            _context.SaveChanges();
            return shop;
        }

        public RewardsShop? GetShopById(int id)
        {
            return _context.RewardsShops.FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<RewardsShop> GetAllShops()
        {
            return _context.RewardsShops.ToList();
        }

        public bool UpdateShop(RewardsShop updatedShop)
        {
            if (updatedShop == null) throw new ArgumentNullException(nameof(updatedShop));
            var existingShop = _context.RewardsShops.FirstOrDefault(s => s.ID == updatedShop.ID);
            if (existingShop == null) return false;
            existingShop.Name = updatedShop.Name;
            existingShop.Description = updatedShop.Description;
            existingShop.Price = updatedShop.Price;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteShop(int id)
        {
            var shop = _context.RewardsShops.FirstOrDefault(s => s.ID == id);
            if (shop == null) return false;
            _context.RewardsShops.Remove(shop);
            _context.SaveChanges();
            return true;
        }
    }
}
