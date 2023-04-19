using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Web.Areas.Dashboard.ViewModels;
using Web.Data;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class QuestionController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
         
            var question = _context.Questions.ToList();
            var questionAnswers = _context.QuestionAnswers.Include(x => x.Answer).ToList();

            QuestionVM vm = new()
            {
                Questions = question,
                QuestionAnswers = questionAnswers,
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()

        {
            ViewData["Answ"] = await _context.Answers.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question, int[] ansIds)
        {
            try
            {
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
                for (int i = 0; i < ansIds.Length; i++)
                {
                    QuestionAnswer questionAnswer = new()
                    {
                        QuestionId = question.Id,
                        AnswerId = ansIds[i]
                    };
                    await _context.QuestionAnswers.AddAsync(questionAnswer);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //public async Task<IActionResult> Create(Question question, List<string> ansIds)
        //{
        //    try
        //    {

        //        //for (int i = 0; i < ansIds.Count; i++)
        //        //{
        //        //    Question questionAnswer = new()
        //        //    {
        //        //        Answers = ansIds[i]
        //        //    };
        //        //    await _context.Questions.AddAsync(questionAnswer);
        //        //    await _context.SaveChangesAsync();
        //        //}
        //        Question questionAnswer = new()
        //        {
        //            //List<string> Answers = ansIds[i]
        //            Id=question.Id,
        //            Content=question.Content,
        //        };
        //        await _context.Questions.AddAsync(questionAnswer);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {

                var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
                var answers = _context.Answers.ToList();
                var questionAnswers = _context.QuestionAnswers.Where(x => x.QuestionId == question.Id).ToList();


                QuestionEditVM editVM = new()
                {
                    Questions = question,
                    Answers = answers,
                    QuestionAnswers=questionAnswers,
                };
                return View(editVM);

            }
            catch (Exception e)
            {

                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Question question, int[] ansIds)
        {
            //student.UpdatedDate = DateTime.Now;
            //_context.Students.Update(student);
            //var studentGroup = _context.StudentGroups.Where(x => x.StudentId == student.Id).ToList();
            //_context.StudentGroups.RemoveRange(studentGroup);
            //_context.SaveChanges();
            //var gr = _context.Groups.FirstOrDefault(x => x.Id == groupId);
            //StudentGroup stgr = new()
            //{
            //    StudentId = student.Id,
            //    GroupId = gr.Id,
            //};
            //_context.StudentGroups.Add(stgr);
            //_context.SaveChanges();
            //return RedirectToAction("Index");

            try
            {
                _context.Questions.Update(question);
                var questionAnswers = _context.QuestionAnswers.Where(x => x.QuestionId == question.Id).ToList();
                _context.QuestionAnswers.RemoveRange(questionAnswers);
                await _context.SaveChangesAsync();

                for (int i = 0; i < ansIds.Length; i++)
                {
                    QuestionAnswer questionAnswer = new()
                    {
                        QuestionId=question.Id,
                        AnswerId = ansIds[i]
                    };
                    await _context.QuestionAnswers.AddAsync(questionAnswer);
                    await _context.SaveChangesAsync();
                }
                //Question questionAnswer = new()
                //{
                //    //List<string> Answers = ansIds[i]
                //    Answers = ansIds
                //};
                //await _context.Questions.AddAsync(questionAnswer);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(x => x.Id == id);
            return View(question);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Question question)
        {
            try
            {
                var qt = await _context.Questions.SingleOrDefaultAsync(x => x.Id == question.Id);
                qt.IsDeleted = true;
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