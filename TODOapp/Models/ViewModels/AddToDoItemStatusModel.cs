using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models.ViewModels
{
    public class AddTodoItemStatusModel
    {
        public int Id { get; set; } 
        
        [Required]
        [Display(Name = "Todo Status")]
        
        public TodoStatus TodoStatus { get; set; }
        public int TodoStatusId { get; set; }

        [Required]
        public string Title { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<SelectListItem> StatusOptions { get; set; }
    }
}
