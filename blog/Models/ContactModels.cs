﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace blog.Models
{
    public class ContactModels
    {
            [Required(ErrorMessage = "First Name is required")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Last Name is required")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Comment is required")]
            public string Comment { get; set; }
    }
}