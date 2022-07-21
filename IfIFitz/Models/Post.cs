﻿using System;
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
 
        public int SizeId { get; set; }
 
        public int MaterialId { get; set; }

        public UserProfile UserProfile { get; set; }

        public Size Size { get; set; }

        public Material Material { get; set; }
    }
}
