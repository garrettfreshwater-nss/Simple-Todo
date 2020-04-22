using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace TODOapp.Models
{
    public class ToDoStatus
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ToDoStatus")]
        public string Title { get; set; }

    }
}
