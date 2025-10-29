// ===== Models/Complaint.cs का पूरा कोड =====
using ComplaintSystem.Data; // <-- यह लाइन एरर को ठीक करेगी
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplaintSystem.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        [Required(ErrorMessage = "Please select a type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";

        // यह वह लाइन है जिसे हमने पिछली बार '?' हटाकर ठीक किया था
        public string AdminRemarks { get; set; } 

        // --- User के साथ रिश्ता (Foreign Key) ---
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } // यह लाइन अब 'ApplicationUser' को ढूँढ पाएगी
    }
}
// ===========================================