using ExaminationSystem.Data;
using ExaminationSystem.Helper;
using ExaminationSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public StudentController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var students = _context.Students.Include(x => x.Groups).ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Groups"] = _context.Groups.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Students student, string groupId, IFormFile NewPhoto)
        {
            student.Picture = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
            student.GroupId = Guid.Parse(groupId);
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            ViewData["Groups"] = _context.Groups.ToList();
            var student = _context.Students.SingleOrDefault(x => x.Id == Guid.Parse(id));
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Students student, IFormFile NewPhoto, string OldPhoto)
        {
            ViewData["Groups"] = _context.Groups.ToList();
            try
            {
                if (NewPhoto != null)
                {
                    student.Picture= ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                }
                else
                {
                    student.Picture = OldPhoto;
                }
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id, Students student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
