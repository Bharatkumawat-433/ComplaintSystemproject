// ===== Pages/AddComplaint.cshtml.cs का पूरा कोड =====
using ComplaintSystem.Data;
using ComplaintSystem.Models;
using Microsoft.AspNetCore.Authorization; // [Authorize] के लिए
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims; // User ID पाने के लिए

namespace ComplaintSystem.Pages
{
    // यह पेज सिर्फ Logged-in यूजर (User या Admin) ही देख सकते हैं
    [Authorize(Roles = "User,Admin")]
    public class AddComplaintModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // [BindProperty] का मतलब है कि यह HTML फॉर्म से डेटा लेगा
        [BindProperty]
        public Complaint NewComplaint { get; set; }

        // Constructor: ज़रूरी services को इंजेक्ट करता है
        public AddComplaintModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            // पेज के लोड होने पर (अभी कुछ नहीं करना)
        }

        // जब यूजर 'Submit' बटन दबाएगा, तब यह कोड चलेगा
        public async Task<IActionResult> OnPostAsync()
        {
            // ModelState.IsValid चेक करता है कि फॉर्म में [Required] फील्ड्स भरे हैं या नहीं
            if (!ModelState.IsValid)
            {
                return Page(); // अगर फॉर्म सही नहीं भरा है, तो पेज वापस दिखाओ
            }

            // वर्तमान (current) Logged-in यूजर की ID ढूँढें
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // अगर किसी वजह से User ID नहीं मिली, तो लॉगिन पेज पर वापस भेज दो
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // कंप्लेंट ऑब्जेक्ट में बाकी की जानकारी सेट करें
            NewComplaint.UserId = userId;
            NewComplaint.Status = "Pending"; // डिफ़ॉल्ट स्टेटस
            NewComplaint.Date = DateTime.Now; // अभी का समय

            // डेटाबेस में नई कंप्लेंट जोड़ें
            _context.Complaints.Add(NewComplaint);
            
            // डेटाबेस में सेव (commit) करें
            await _context.SaveChangesAsync();

            // कंप्लेंट सबमिट होने के बाद, यूज़र को उसके डैशबोर्ड पर भेजें
            // (हम UserDashboard पेज अगले स्टेप में बनाएँगे)
            return RedirectToPage("./UserDashboard"); 
        }
    }
}
// ======================================================