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
    public class CategoryController : Controller
    {
        private CategoryServices _categoryServices;
        private SubjectServices _subjectServices;
        public CategoryController(CategoryServices categoryServices, SubjectServices subjectServices)
        {
            _categoryServices = categoryServices;
            _subjectServices = subjectServices;
        }

        // GET: Des
        public async Task<ActionResult> Index(int? page = 1, string keyWord = null)
        {
            ViewBag.Keyword = keyWord;
            return View(await _categoryServices.GetAll(page.Value, keyWord: keyWord));
        }

        // GET: Des/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CATEGORY dE = _categoryServices.GetById(id.Value);
            if (dE == null)
            {
                return NotFound();
            }
            return View(dE);
        }

        // GET: Des/Create
        public ActionResult Create()
        {
            ViewBag.SubjectList = new SelectList(_subjectServices.GetAll(), "Value", "Text");

            return View();
        }

        // POST: Des/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATEGORY category)
        {
            if (ModelState.IsValid)
            {
                _categoryServices.Add(category);
                return RedirectToAction("Index");
            }
            ViewBag.SubjectList = new SelectList(_subjectServices.GetAll(), "Value", "Text", category.SubjectId);
            return View(category);
        }

        // GET: Des/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _categoryServices.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.SubjectList = new SelectList(_subjectServices.GetAll(), "Value", "Text", model.SubjectId);
            return View(model);
        }

        // POST: Des/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CATEGORY category)
        {
            if (ModelState.IsValid)
            {
                _categoryServices.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.SubjectList = new SelectList(_subjectServices.GetAll(), "Value", "Text", category.SubjectId);

            return View(category);
        }

        // POST: Des/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _categoryServices.Delete(id);
            return Ok(result);
        }
    }
}
