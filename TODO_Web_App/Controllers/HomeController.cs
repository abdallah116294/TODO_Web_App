using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TODO_Web_App.Models;

namespace TODO_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private  ToDoContext _context;
  
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ToDoContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.categories = _context.Categories.ToList();
            ViewBag.statuses = _context.Statuses.ToList();
            ViewBag.dueFilters = Filters.dueFilterValues;

            IQueryable<ToDO> query = _context.ToDOs
                .Include(t => t.category)
                .Include(t => t.status);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.categoryID == filters.categoryID);
            }

            if (filters.HasStatus)
            {
                query = query.Where(t => t.statusID == filters.statusID);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.isPast)
                {
                    query = query.Where(t => t.dueDate < today);
                }
                else if (filters.isFuture)
                {
                    query = query.Where(t => t.dueDate > today);
                }
                else if (filters.isToday)
                {
                    query = query.Where(t => t.dueDate == today);
                }
            }

            var tasks = query.OrderBy(t => t.dueDate).ToList();


            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = _context.Categories.ToList();
            ViewBag.statuses = _context.Statuses.ToList();

            var task = new ToDO
            {
                statusID = "open",
            };

            return View(task);
        }
        [HttpPost]
        public IActionResult Add(ToDO task)
        {
            if (ModelState.IsValid)
            {
                _context.ToDOs.Add(task);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.categories = _context.Categories.ToList();
                ViewBag.statuses = _context.Statuses.ToList();
                return View(task);
            }
        }
        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult markComplete([FromRoute] string id, ToDO selected)
        {
            selected = _context.ToDOs.Find(selected.ID);
            if (selected != null)
            {
                selected.statusID = "closed";
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult deleteComplete(string id)
        {
            var toDelete = _context.ToDOs.Where(t => t.statusID == "closed").ToList();

            foreach (var task in toDelete)
            {
                _context.ToDOs.Remove(task);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
