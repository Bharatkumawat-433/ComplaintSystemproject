using Microsoft.AspNetCore.Identity;

namespace ComplaintSystem.Data
{
    // हम IdentityUser क्लास को बढ़ा रहे हैं (extending)
    public class ApplicationUser : IdentityUser
    {
        // ये दो नई फील्ड्स हैं जो हम User टेबल में जोड़ना चाहते हैं
        public string Name { get; set; }
        public string Role { get; set; }
    }
}