// ===== Pages/AdminDashboard.cshtml.cs का पूरा कोड =====
using ComplaintSystem.Data;
using ComplaintSystem.Models;
using Microsoft.AspNetCore.Authorization; // [Authorize] के लिए
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // .Include() के लिए
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintSystem.Pages
{
    // यह पेज सिर्फ "Admin" रोल वाले ही देख सकते हैं
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // यह लिस्ट HTML पेज पर दिखाई जाएगी
        public List<Complaint> AllComplaints { get; set; }

        public AdminDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // जब पेज लोड होगा (GET), तब यह कोड चलेगा
        public async Task OnGetAsync()
        {
            // डेटाबेस से सारी कंप्लेंट्स लाओ
            AllComplaints = await _context.Complaints
                                      .Include(c => c.User) // <-- ज़रूरी: कंप्लेंट के साथ User की जानकारी (Name/Email) भी लाओ
                                      .OrderByDescending(c => c.Date) // सबसे नई वाली पहले
                                      .ToListAsync();
        }
    }
}
// ======================================================