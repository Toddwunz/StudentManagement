﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StudentManagement.Models;

namespace StudentManagement.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Please input name")]
        public string Name { get; set; }

        [Display(Name = "Class Name")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9_+-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Wrong Email Format")]
        public string Email { get; set; }

        [Display(Name = "Profile photo")]
        public IFormFile Photopath { get; set; }
    }
}
