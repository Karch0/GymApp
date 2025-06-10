using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models.Data
{
    public class GymLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Името на локацията не може да е по-дълго от 15 символа.")]
        public string Name { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Капацитетът трябва да е между 0 и 150.")]
        public int Capacity { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }

        public ICollection<Coach> Coaches { get; set; }

        public GymLocation() { }

        public GymLocation(string name, int capacity, ICollection<Subscription> subscriptions, ICollection<Coach> coaches)
        {
            Name = name;
            Capacity = capacity;
            Subscriptions = subscriptions;
            Coaches = coaches;
        }
    }
}
