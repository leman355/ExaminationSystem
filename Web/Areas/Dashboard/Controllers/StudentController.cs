using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Web.Areas.Dashboard.DTOs;
using Web.Areas.Dashboard.ViewModels;
using Web.Data;
using Web.Helper;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
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
            //var stl = _context.Students.Include(x=>x.User);
            ////var stl = _context.Students.Include(x=>x.User).Where(x => x.IsDeleted == false).ToList();
            //return View(stl);

            var students = _context.Students.Include(x => x.User).ToList();
            var studentGroups = _context.StudentGroups.Include(x => x.Group).ToList();

            StudentVM vm = new()
            {
                Students = students,
                StudentGroups = studentGroups,
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var group = _context.Groups.Where(x => x.IsDeleted == false).ToList();

            if (group.Count == 0)
            {
                return RedirectToAction("Create", "Group");
            }

            //ViewData["StudentUser"] = _context.Users.Where(x => x.Id == student.UserId);
            ViewData["Groups"] = _context.Groups.Where(x => x.IsDeleted == false).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student, IFormFile NewPhoto, int groupId, bool IsDeleted)
        {
            student.IsDeleted = IsDeleted;
            student.Picture = ImageHelper.UploadImage(NewPhoto, _webHostEnvironment);
            //student.UserId = userId;
            student.CreatedDate = DateTime.Now;
            student.UpdatedDate = DateTime.Now;
            _context.Students.Add(student);
            _context.SaveChanges();
            var gr = _context.Groups.FirstOrDefault(x => x.Id == groupId);
            StudentGroup studentGroup = new()
            {
                StudentId = student.Id,
                GroupId = gr.Id,
            };
            _context.StudentGroups.Add(studentGroup);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Student studentusr, int id)
        {
            var gr = _context.Groups.Where(x => x.IsDeleted == false).ToList();

            if (gr.Count == 0)
            {
                return RedirectToAction("Create", "Group");
            }

            ViewData["StudentUser"] = _context.Users.Where(x => x.Id == studentusr.UserId);
            //var student = _context.Students.SingleOrDefault(x => x.Id.ToString() == id);
            //return View(student);
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            var group = _context.Groups.Where(x => x.IsDeleted == false).ToList();
            var studentGroup = _context.StudentGroups.Where(x => x.StudentId == student.Id).ToList();

            StudentEditVM editVM = new()
            {
                Students = student,
                Groups = group,
                StudentGroups = studentGroup,
            };
            return View(editVM);
        }
        [HttpPost]
        public IActionResult Edit(Student student, IFormFile NewPhoto, string OldPhoto, int groupId, bool IsDeleted)
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

                DateTime createdDate = _context.Students.AsNoTracking().FirstOrDefault(x => x.Id == student.Id)?.CreatedDate ?? DateTime.MinValue;
                if (createdDate != DateTime.MinValue)
                {
                    student.CreatedDate = createdDate;
                }
                _context.Entry(student).Property(x => x.CreatedDate).IsModified = false;

                student.IsDeleted = IsDeleted;
                student.UpdatedDate = DateTime.Now;
                _context.Students.Update(student);
                var studentGroup = _context.StudentGroups.Where(x => x.StudentId == student.Id).ToList();
                _context.StudentGroups.RemoveRange(studentGroup);
                _context.SaveChanges();
                var gr = _context.Groups.FirstOrDefault(x => x.Id == groupId);
                StudentGroup stgr = new()
                {
                    StudentId = student.Id,
                    GroupId = gr.Id,
                };
                _context.StudentGroups.Add(stgr);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            return View(student);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            try
            {
                var st = await _context.Students.SingleOrDefaultAsync(x => x.Id == student.Id);
                st.IsDeleted = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
