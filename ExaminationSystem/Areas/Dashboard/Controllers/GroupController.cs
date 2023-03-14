using Azure;
using ExaminationSystem.Data;
using ExaminationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GroupController : Controller
    {
        private readonly AppDbContext _context;

        public GroupController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var group = _context.Groups.ToList();
            return View(group);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Groups group)
        {
            try
            {
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var edit = _context.Groups.FirstOrDefault(e => e.Id.ToString() == id);
            _context.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Groups group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var del = await _context.Groups.SingleOrDefaultAsync(x => x.Id.ToString() == id);
            return View(del);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Groups group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
