using ComplaintSystem.Models; // Complaint Model के लिए
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComplaintSystem.Data
{
    // IdentityDbContext<ApplicationUser> से बदलें
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // यह नई लाइन जोड़ें
        public DbSet<Complaint> Complaints { get; set; }
    }
}