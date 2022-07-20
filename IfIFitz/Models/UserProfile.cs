using System.ComponentModel.DataAnnotations;

namespace IfIFitz.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        
        [StringLength(28, MinimumLength = 28)]
        public string FirebaseUserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        [MaxLength(255)]
        public string ImageLocation { get; set; }
        
        public bool IsActive { get; set; }
    }
}
