using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models.ViewModels
{
    public class AddTodoItemStatusModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "ToDoStatus")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public int TodoStatusId { get; set; }

        public List<>  { get; set; }
    }
}
