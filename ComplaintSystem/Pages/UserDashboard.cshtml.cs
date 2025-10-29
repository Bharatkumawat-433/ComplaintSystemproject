// ===== Pages/UserDashboard.cshtml.cs का पूरा कोड =====
using ComplaintSystem.Data;
using ComplaintSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComplaintSystem.Pages
{
    // यह पेज सिर्फ Logged-in यूजर ही देख सकते हैं
    [Authorize(Roles = "User,Admin")]
    public class UserDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // यह लिस्ट HTML पेज पर दिखाई जाएगी
        public List<Complaint> Complaints { get; set; }

        public UserDashboardModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // जब पेज लोड होगा (GET), तब यह कोड चलेगा
        public async Task OnGetAsync()
        {
            // वर्तमान (current) Logged-in यूजर की ID ढूँढें
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // डेटाबेस से सिर्फ इस यूज़र की कंप्लेंट्स लाओ
            Complaints = await _context.Complaints
                                 .Where(c => c.UserId == userId) // सिर्फ इस यूज़र की
                                 .OrderByDescending(c => c.Date) // सबसे नई वाली पहले
                                 .ToListAsync();
        }
    }
}
// ======================================================