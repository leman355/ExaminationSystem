using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
            var answer = _context.Answers.ToList();

            QuestionVM vm = new()
            {
                Questions = question,
                QuestionAnswers = questionAnswers,
                Answers = answer,
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            //ViewData["Answ"] = await _context.Answers.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question, string[] Option, bool IsDeleted)
        {
            try
            {
                question.IsDeleted = IsDeleted;
                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();
                for (int i = 0; i < Option.Length; i++)
                {
                    if (Option[i] != null)
                    {
                        bool.TryParse(Request.Form[$"Status_{i}"], out bool status);
                        Answer answer = new()
                        {
                            Option = Option[i],
                            Status = status,
                        };
                        await _context.Answers.AddAsync(answer);
                        await _context.SaveChangesAsync();

                        QuestionAnswer questionAnswer = new()
                        {
                            QuestionId = question.Id,
                            AnswerId = answer.Id
                        };
                        await _context.QuestionAnswers.AddAsync(questionAnswer);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

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
                    QuestionAnswers = questionAnswers,
                };

                return View(editVM);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question, string[] Option, bool IsDeleted)
        {
            try
            {
                question.IsDeleted = IsDeleted;
                _context.Questions.Update(question);
                await _context.SaveChangesAsync();
                
                var questionAnswers = _context.QuestionAnswers.Where(x => x.QuestionId == question.Id).ToList();
                _context.QuestionAnswers.RemoveRange(questionAnswers);
                await _context.SaveChangesAsync();

            
                for (int i = 0; i < Option.Length; i++)
                {
                    if (Option[i] != null)
                    {
                        bool.TryParse(Request.Form[$"Status_{i}"], out bool status);
                        Answer answer = new()
                        {
                            Option = Option[i],
                            Status = status,
                        };
                        await _context.Answers.AddAsync(answer);
                        await _context.SaveChangesAsync();

                        QuestionAnswer questionAnswer = new()
                        {
                            QuestionId = question.Id,
                            AnswerId = answer.Id
                        };
                        await _context.QuestionAnswers.AddAsync(questionAnswer);
                        await _context.SaveChangesAsync();
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
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