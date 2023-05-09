using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    var roles = _context.Roles.ToList();
        //    var model = roles.Select(role => new Role { Id = role.Id, Name = role.Name }).ToList();
        //    return View(model);
        //    //var role = _context.Roles.ToList();
        //    //return View(role);
        //}
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Role role, bool IsDeleted)
        //{
        //    try
        //    {
        //        _context.Roles.Add(role);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpGet]
        //public IActionResult Edit(string id)
        //{
        //    var edit = _context.Roles.FirstOrDefault(e => e.Id == id);
        //    _context.SaveChanges();
        //    return View(edit);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(Role role)
        //{
        //    try
        //    {
        //        _context.Roles.Update(role);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpGet]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var del = await _context.Roles.SingleOrDefaultAsync(x => x.Id == id);
        //    return View(del);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Delete(Role role)
        //{
        //    _context.Roles.RemoveRange(role);
        //    _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            var model = roles.Select(role => new Role { Id = role.Id, Name = role.Name }).ToList();
            return View(model);
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var edit = _context.Roles.FirstOrDefault(e => e.Id == id);
            var role = new Role { Id = edit.Id, Name = edit.Name };
            return View(role);
            //var edit = _context.Roles.FirstOrDefault(e => e.Id == id);
            //return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var identityRole = await _context.Roles.SingleOrDefaultAsync(x => x.Id == id);
            var role = new Role { Id = identityRole.Id, Name = identityRole.Name };
            return View(role);
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var del = await _context.Roles.SingleOrDefaultAsync(x => x.Id == id);
        //    return View(del);
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(Role role)
        {
            var identityRole = await _context.Roles.SingleOrDefaultAsync(x => x.Id == role.Id);
            _context.Roles.Remove(identityRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //[HttpPost]
        //public async Task<IActionResult> Delete(Role role)
        //{
        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
