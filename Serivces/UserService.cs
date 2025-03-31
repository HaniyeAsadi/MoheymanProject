using System;
using System.Linq;
using MoheymanProject.Data;
using MoheymanProject.Models;

namespace MoheymanProject.Services
{
    public class UserService
    {
        public readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public void Register(string username, string password)
        {
            if (_db.Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Register failed! Username already exists.");
                return;
            }

            _db.Users.Add(new User { Username = username, Password = password, Status = true });
            _db.SaveChanges();
            Console.WriteLine("Registered successfully!");
        }
    }

}
