using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoItems
        public async Task<ActionResult> Index(string filter)
        {
            var user = await GetCurrentUserAsync();
            var items = await _context.TodoItems
                .Where(ti => ti.ApplicationUserId == user.Id)
                .Include(ti => ti.TodoStatus)
                .ToListAsync();

            switch (filter)
            {
                case "Todo":
                    items = await _context.TodoItems
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 1)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "In Progress":
                    items = await _context.TodoItems
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 2)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "Done":
                    items = await _context.TodoItems
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Where(ti => ti.TodoStatusId == 3)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
                case "All":
                    items = await _context.TodoItems
                        .Where(ti => ti.ApplicationUserId == user.Id)
                        .Include(ti => ti.TodoStatus)
                        .ToListAsync();
                    break;
            }

            return View(items);
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoItems/Create
        public async Task<ActionResult> Create()
        {
            var allStatuses = await _context.TodoStatus
                .Select(td => new SelectListItem() { Text = td.Status, Value = td.Id.ToString() })
                .ToListAsync();

            var viewModel = new AddTodoItemStatusModel();

            viewModel.StatusOptions = allStatuses;

            return View(viewModel);
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddTodoItemStatusModel addTodoItemStatusModel)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    Title = addTodoItemStatusModel.Title,
                    TodoStatusId = addTodoItemStatusModel.TodoStatusId
                };

                var user = await GetCurrentUserAsync();
                todoItem.ApplicationUserId = user.Id;

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allStatuses = await _context.TodoStatus
                .Select(td => new SelectListItem() { Value = td.Status, Value = td.Id.ToString() })
                .ToListAsync();

            var todoItem = _context.TodoItems.FirstOrDefault(ti => ti.Id == id);

            var viewModel = new AddTodoItemStatusModel()
            {
                Title = todoItem.Title,
                TodoStatusId = todoItem.TodoStatusId,
                ApplicationUserId = todoItem.ApplicationUserId,
                StatusOptions = allStatuses
            };

            return View(viewModel);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, AddTodoItemStatusModel addTodoItemStatusModel)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var todoItem = new TodoItem()
                {
                    Id = id,
                    Title = addTodoItemStatusModel.Title,
                    TodoStatusId = addTodoItemStatusModel.TodoStatusId,
                    ApplicationUserId = user.Id
                };

                _context.TodoItems.Update(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItems/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var todoItem = await _context.TodoItems.Include(ti => ti.TodoStatus).FirstOrDefaultAsync(ti => ti.Id == id);

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TodoItem todoItem)
        {
            try
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}