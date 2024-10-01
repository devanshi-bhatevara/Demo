﻿using System.ComponentModel.DataAnnotations;

namespace DemoCrudMVC.Models
{
    public class AddDemoViewModel
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }
    }
}
