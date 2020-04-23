﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class TodoStatus
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "TodoStatus")]
        public string Title { get; set; }

    }
}
