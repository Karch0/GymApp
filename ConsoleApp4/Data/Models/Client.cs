using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models.Data
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Range(0, 1000, ErrorMessage = "Точките трябва да са между 0 и 1000.")]
        public int Points { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "Имейлът трябва да е между 10 и 25 символа.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        public string Email { get; set; }

        public int? CoachId { get; set; }
        public Coach? Coach { get; set; }

        public Subscription? Subscription { get; set; }

        public Client() { }

        public Client (string fullName, int points, string email, int? coachId, Coach? coach, Subscription? subscription)
        {
            FullName = fullName;
            Points = points;
            Email = email;
            CoachId = coachId;
            Coach = coach;
            Subscription = subscription;
        }
    }
}
