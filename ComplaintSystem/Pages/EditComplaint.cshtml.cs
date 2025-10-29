
using ComplaintSystem.Data;
using ComplaintSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComplaintSystem.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditComplaintModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditComplaintModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Complaint Complaint { get; set; }

       
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Complaint = await _context.Complaints
                                .Include(c => c.User) 
                                .FirstOrDefaultAsync(m => m.ComplaintId == id);

            if (Complaint == null)
            {
                return NotFound();
            }
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            
            ModelState.Remove("Complaint.Type");
            ModelState.Remove("Complaint.Description");
            ModelState.Remove("Complaint.UserId");
            ModelState.Remove("Complaint.User");


            if (!ModelState.IsValid)
            {
               
                var originalComplaint = await _context.Complaints
                                                .Include(c => c.User)
                                                .AsNoTracking() 
                                                .FirstOrDefaultAsync(c => c.ComplaintId == Complaint.ComplaintId);
                
                if (originalComplaint != null)
                {
                    Complaint.User = originalComplaint.User;
                    Complaint.Description = originalComplaint.Description;
                    Complaint.Date = originalComplaint.Date;
                    Complaint.Type = originalComplaint.Type;
                }
                
                return Page(); 
            }

         
            var complaintFromDb = await _context.Complaints.FindAsync(Complaint.ComplaintId);

            if (complaintFromDb == null)
            {
                return NotFound();
            }

           
            complaintFromDb.Status = Complaint.Status;
            complaintFromDb.AdminRemarks = Complaint.AdminRemarks;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

           
            return RedirectToPage("./AdminDashboard");
        }
    }
}
// ======================================================