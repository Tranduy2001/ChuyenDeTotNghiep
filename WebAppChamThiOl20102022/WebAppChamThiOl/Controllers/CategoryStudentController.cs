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
    public class CategoryStudentController : Controller
    {
        private CategoryStudentServices _categoryStudentServices;
        private SubjectServices _subjectServices;
        private CategoryServices _categoryServices;
        private AccountServices _accountServices;
        public CategoryStudentController(CategoryStudentServices categoryStudentServices, SubjectServices subjectServices, CategoryServices categoryServices, AccountServices accountServices)
        {
            _categoryStudentServices = categoryStudentServices;
            _subjectServices = subjectServices;
            _categoryServices = categoryServices;
            _accountServices = accountServices;
        }

        // GET: Des
        public async Task<ActionResult> Index(int? page = 1, string keyWord = null)
        {
            ViewBag.Keyword = keyWord;
            return View(await _categoryStudentServices.GetAll(page.Value, keyWord: keyWord));
        }

        // GET: Des/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _categoryStudentServices.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Des/Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text");
            ViewBag.UserList = new SelectList(_accountServices.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Des/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LOG_THI model)
        {
            if (model.DeId == 0)
                ModelState.AddModelError("DeId", "Trường này không được để trống!");
            if (model.UserId == 0)
                ModelState.AddModelError("UserId", "Trường này không được để trống!");
            if (ModelState.IsValid)
            {
                _categoryStudentServices.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", model.DeId);
            ViewBag.UserList = new SelectList(_accountServices.GetAll(), "Id", "FullName", model.UserId);
            //ViewBag.SubjectList = new SelectList(_subjectServices.GetAll(), "Value", "Text", category.SubjectId);
            return View(model);
        }

        // GET: Des/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = _categoryStudentServices.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", model.DeId);
            ViewBag.UserList = new SelectList(_accountServices.GetAll(), "Id", "FullName", model.UserId);
            return View(model);
        }

        // POST: Des/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LOG_THI model)
        {
            if (ModelState.IsValid)
            {
                _categoryStudentServices.Update(model);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = new SelectList(_categoryServices.GetAllSelect(), "Value", "Text", model.DeId);
            ViewBag.UserList = new SelectList(_accountServices.GetAll(), "Id", "FullName", model.UserId);
            return View(model);
        }

        // POST: Des/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _categoryStudentServices.Delete(id);
            return Ok(result);
        }
    }
}
