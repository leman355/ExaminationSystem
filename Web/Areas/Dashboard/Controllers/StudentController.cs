using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Dashboard.DTOs;
using Web.Data;
using Web.Helper;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
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
            var stl = _context.Students.Include(x=>x.User);
            //var stl = _context.Students.Include(x=>x.User).Where(x => x.IsDeleted == false).ToList();
            return View(stl);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Users"] = _context.Users.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student, string userId, IFormFile NewPhoto)
        {
            student.Picture = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
            student.UserId = userId;
            student.StudentCreatedDate = DateTime.Now;
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewData["Users"] = _context.Users.ToList();
            var student = _context.Students.SingleOrDefault(x => x.Id.ToString() == id);
            return View(student);
        }
        public IActionResult Edit(Student student, IFormFile NewPhoto, string OldPhoto)
        {
            try
            {
                if (NewPhoto != null)
                {
                    student.Picture = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
                }
                else
                {
                    student.Picture = OldPhoto;
                }
                student.IsDeleted = !student.IsDeleted;
                student.StudentCreatedDate = DateTime.Now;
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
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id.ToString() == id);
            return View(student);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id, Student student)
        {
            try
            {
                var st = await _context.Students.SingleOrDefaultAsync(x => x.Id == student.Id);
                st.IsDeleted = true;
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
