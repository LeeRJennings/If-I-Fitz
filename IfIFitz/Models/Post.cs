using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IfIFitz.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public int UserProfileId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public string ImageLocation { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        public UserProfile UserProfile { get; set; }

        public Size Size { get; set; }

        public Material Material { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
