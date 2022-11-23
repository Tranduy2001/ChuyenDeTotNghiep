using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAppChamThiOl.Controllers
{
    public class ResultQuizController : Controller
    {
        private ResultQuizServices _resultQuizServices;
        public ResultQuizController(ResultQuizServices resultQuizServices)
        {
            _resultQuizServices = resultQuizServices;
        }
        // GET: ResultQuizs
        public ActionResult Index(int? quizId)
        {
            ViewBag.quizId = quizId;
            if (quizId.HasValue)
            {
                return View(_resultQuizServices.GetDapAnByCauHoiId(quizId.Value));
            }
            else
            {
                return RedirectToAction("Index", "Quiz");
            }
        }

        // GET: ResultQuizs/Create
        public ActionResult Create(int quizId)
        {
            //ViewBag.CauHoiId = new SelectList(_resultQuizServices.GetAllCauHoi(), "Id", "Ten");
            return View();
        }

        // POST: ResultQuizs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RESULT_QUIZ dAP_AN_CAU_HOI)
        {
            if (ModelState.IsValid)
            {
                _resultQuizServices.Add(dAP_AN_CAU_HOI);
                return RedirectToAction("Index", "ResultQuiz", new { quizId = dAP_AN_CAU_HOI.QuizId });
            }

            //ViewBag.CauHoiId = new SelectList(_resultQuizServices.GetAllCauHoi(), "Id", "Ten", dAP_AN_CAU_HOI.CauHoiId);
            return View(dAP_AN_CAU_HOI);
        }

        // GET: ResultQuizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RESULT_QUIZ dAP_AN_CAU_HOI = _resultQuizServices.GetById(id.Value);
            if (dAP_AN_CAU_HOI == null)
            {
                return NotFound();
            }
            ViewBag.CauHoiId = _resultQuizServices.GetAllCauHoi();
            return View(dAP_AN_CAU_HOI);
        }

        // POST: ResultQuizs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RESULT_QUIZ dAP_AN_CAU_HOI)
        {
            if (ModelState.IsValid)
            {
                _resultQuizServices.Update(dAP_AN_CAU_HOI);
                return RedirectToAction("Index", "ResultQuiz", new { cauHoiId = dAP_AN_CAU_HOI.QuizId });
            }
            ViewBag.CauHoiId = _resultQuizServices.GetAllCauHoi();
            return View(dAP_AN_CAU_HOI);
        }


        // POST: ResultQuizs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _resultQuizServices.Delete(id);
            return Ok(result);
        }
    }
}
