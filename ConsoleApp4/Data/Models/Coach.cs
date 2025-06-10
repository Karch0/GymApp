using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models.Data
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Името не може да е по-дълго от 20 символа!")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^(Expert|Advanced|Intermediate|Novice)$",
            ErrorMessage = "Статусът трябва да бъде: Expert, Advanced, Intermediate или Novice!")]
        public string Status { get; set; }
        [Required]
        [Range(0, 50, ErrorMessage = "Опитът трябва да е между 0 и 50 години!")]
		public int? ExperienceYears { get; set; }

		[Required]
        public int TrainingPrice { get; set; }
        public ICollection<Client> Clients { get; set; }

        [Required]
        public int GymLocationId { get; set; }
        public GymLocation GymLocation { get; set; }
        

        public Coach() { }

        public Coach(string fullName, int Expirience, int trainingPrice, 
            ICollection<Client> clients, int gymLocationId, GymLocation gymLocation)
        {
            FullName = fullName;
			switch (Expirience)
			{
				case int n when (n >= 1 && n < 3):
					Status = "Novice";
					break;
				case int n when (n >= 3 && n <= 5):
					Status = "Intermediate";
					break;
				case int n when (n >= 7 && n <= 15):
					Status = "Advanced";
					break;
				case int n when (n > 15 && n <= 50):
					Status = "Expert";
					break;
				default:
					Status = "Unknown";
					break;
			}
			TrainingPrice = trainingPrice;
            Clients = clients;
            GymLocationId = gymLocationId;
            GymLocation = gymLocation;
        }
    }
}
