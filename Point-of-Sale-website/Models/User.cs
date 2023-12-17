using Microsoft.EntityFrameworkCore;

namespace Point_of_Sale_website.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public bool IsLocked { get; set; }

        public DbSet<Role> Roles { get; set; }

    }
}