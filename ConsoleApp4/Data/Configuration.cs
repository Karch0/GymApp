using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Models
{
    public class Configuration
    {   
        public const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=GymDataBase;Integrated Security=True;" +
                                         "Encrypt=True;TrustServerCertificate=True;"; 
    }
}
