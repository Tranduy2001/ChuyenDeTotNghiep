#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppChamThiOl.Data;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Services;

namespace WebAppChamThiOl.Controllers
{
    public class SubjectController : Controller
    {
        //private readonly DataContext _context;
        private readonly SubjectServices _subjectServices;

        public SubjectController(SubjectServices subjectServices)
        {
            _subjectServices = subjectServices;
        }

        public async Task<IActionResult> Index(int? page = 1, string keyword = null )
        {
            var model = await _subjectServices.GetAll(page: page.Value, keyWord: keyword);
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gENRE = _subjectServices.GetById(id.Value);
            if (gENRE == null)
            {
                return NotFound();
            }

            return View(gENRE);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SUBJECT gENRE)
        {
            if (ModelState.IsValid)
            {
                _subjectServices.Add(gENRE);
                return RedirectToAction(nameof(Index));
            }
            return View(gENRE);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gENRE = _subjectServices.GetById(id.Value);
            if (gENRE == null)
            {
                return NotFound();
            }
            return View(gENRE);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SUBJECT gENRE)
        {
            if (id != gENRE.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _subjectServices.Update(gENRE);
                return RedirectToAction(nameof(Index));
            }
            return View(gENRE);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _subjectServices.Delete(id);
            return Ok(result);
        }
    }
}
