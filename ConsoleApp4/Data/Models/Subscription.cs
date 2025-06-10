using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models.Data
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int VisitationCounter { get; set; }

        [Required]
        public bool IsInStreak { get; set; }

        public int StreakWeekCounter { get; set; } = 0;

        [Required]
        public int SubscriptionPrice { get; set; }

        [Required]
        public int GymLocationId { get; set; }
        public GymLocation GymLocation { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public Subscription() { }

        public Subscription(DateTime startDate, DateTime endDate, int visitationCounter, bool isInStreak, int subscriptionPrice, 
            int gymLocationId, GymLocation gymLocation,
            int clientId, Client client)
        {
            StartDate = startDate;
            EndDate = endDate;
            VisitationCounter = visitationCounter;
            IsInStreak = isInStreak;
            SubscriptionPrice = subscriptionPrice;
            GymLocationId = gymLocationId;
            GymLocation = gymLocation;
            ClientId = clientId;
            Client = client;
        }
    }
}
