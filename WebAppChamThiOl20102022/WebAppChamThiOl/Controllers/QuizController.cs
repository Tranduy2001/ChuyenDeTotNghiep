using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppChamThiOl.Controllers
{
    public class QuizController : Controller
    {
        private QuizServices _quizServices;
        private CategoryServices _categoryServices;
        public QuizController(QuizServices quizServices, CategoryServices categoryServices)
        {
            _quizServices = quizServices;
            _categoryServices = categoryServices;
        }
        // GET: quiz
        public async Task<ActionResult> Index(int? page = 1, string keyword = null, int? categoryId = null)
        {
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", categoryId);
            var model = await _quizServices.GetAll(page: page.Value, keyword: keyword, categoryId: categoryId);
            ViewBag.Keyword = keyword;
            return View(model);
        }

        // GET: quiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            QUIZ quiz = _quizServices.GetById(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // GET: quiz/Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text");
            ViewBag.QuizType = new SelectList(_quizServices.GetListQuizType(), "Value", "Text");
            return View();
        }

        // POST: quiz/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QUIZ quiz)
        {
            if (ModelState.IsValid)
            {
                _quizServices.Add(quiz);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", quiz.CategoryId);
            ViewBag.QuizType = new SelectList(_quizServices.GetListQuizType(), "Value", "Text", quiz.QuizType);
            return View(quiz);
        }

        // GET: quiz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            QUIZ quiz = _quizServices.GetById(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", quiz.CategoryId);
            ViewBag.QuizType = new SelectList(_quizServices.GetListQuizType(), "Value", "Text", quiz.QuizType);

            return View(quiz);
        }

        // POST: quiz/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QUIZ quiz)
        {
            if (ModelState.IsValid)
            {
                _quizServices.Update(quiz);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", quiz.CategoryId);
            ViewBag.QuizType = new SelectList(_quizServices.GetListQuizType(), "Value", "Text", quiz.QuizType);

            return View(quiz);
        }

        // POST: quiz/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _quizServices.Delete(id);
            return Ok(result);
        }
    }
}
