using System;
using System.ComponentModel.DataAnnotations;

namespace IfIFitz.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
