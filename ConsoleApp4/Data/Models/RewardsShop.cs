using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Data.Models
{
    public class RewardsShop
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public RewardsShop() { }

        public RewardsShop(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
