using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var role = _context.Roles.ToList();
            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = _context.Roles.FirstOrDefault(e => Int32.Parse(e.Id) == id);
            _context.SaveChanges();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var del = await _context.Roles.SingleOrDefaultAsync(x => Int32.Parse(x.Id) == id);
            return View(del);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Role role)
        {
            try
            {
                var a = await _context.Roles.SingleOrDefaultAsync(x => x.Id == role.Id);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
