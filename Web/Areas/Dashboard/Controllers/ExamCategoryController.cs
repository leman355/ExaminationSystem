using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class ExamCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public ExamCategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var examCategory = _context.ExamCategories.ToList();
            return View(examCategory);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamCategory examCategory)
        {
            try
            {
                _context.ExamCategories.Add(examCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var edit = _context.ExamCategories.FirstOrDefault(e => e.Id == id);
            _context.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ExamCategory examCategory)
        {
            try
            {
                _context.ExamCategories.Update(examCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var del = await _context.ExamCategories.SingleOrDefaultAsync(x => x.Id == id);
            return View(del);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ExamCategory examCategory)
        {
            _context.ExamCategories.RemoveRange(examCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
