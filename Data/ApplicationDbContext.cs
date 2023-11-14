using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Models;

namespace EDalolatnoma.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Dalolatnoma> Dalolatnomlar { get; set; } 
        public DbSet<Field> Field { get; set; }
        public DbSet<Well> Well { get; set; }
        public DbSet<KernoData> KernoData { get; set; }
        public DbSet<WellStatus> WellStatus { get; set; }



    }
}