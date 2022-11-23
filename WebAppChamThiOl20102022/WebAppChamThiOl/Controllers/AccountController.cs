using WebAppChamThiOl.Models;
using WebAppChamThiOl.Services;
using Microsoft.AspNetCore.Mvc;
using static WebAppChamThiOl.Models.Constants;
using Microsoft.Win32;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Data;

namespace WebAppChamThiOl.Controllers
{
    public class AccountController : Controller
    {
        private AccountServices _accountServices;
        public AccountController(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {
            var result = _accountServices.Login(loginView);
            //if (result == LoginStatus.DangNhapThanhCong) return RedirectToAction("Index", "Home");
            switch (result.Code)
            {
                case LoginStatus.DangNhapThanhCong:
                    {
                        HttpContext.Session.Set(Constants.UserIdentity, result.Data);
                        return RedirectToAction("Index", "Home");
                    }
                default:
                    {
                        ModelState.AddModelError("", result.Code.GetDisplayName());
                        return View(loginView);
                    }
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            var result = _accountServices.Register(registerView);
            switch (result.Code)
            {
                case RegisterStatus.DangKyThanhCong:
                    {
                        HttpContext.Session.Set(Constants.UserIdentity, result.Data);
                        return RedirectToAction("Index", "Home");
                    }
                default:
                    ModelState.AddModelError("", result.Code.GetDisplayName());
                    return View(registerView);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Constants.UserIdentity);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, string rankSort = null, string keyWord = null, int? role = null)
        {
            UserIdentity userIdentity = HttpContext.Session.Get<UserIdentity>(Constants.UserIdentity);
            bool isAdmin = false;
            if (userIdentity != null)
            {
                isAdmin = userIdentity.IsAdmin.GetValueOrDefault(false);
            }
            var result = await _accountServices.GetAll(page, pageSize, rankSort, keyWord, isAdmin, role);
            return View(result);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            USER user = _accountServices.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: quiz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: quiz/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateViewModel user)
        {
            if (ModelState.IsValid)
            {
                _accountServices.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: quiz/Edit/5
        public ActionResult Edit(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return NotFound();
            }
            UserUpdateViewModel user = _accountServices.GetByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: quiz/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserUpdateViewModel user)
        {
            if (ModelState.IsValid)
            {
                _accountServices.Update(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: quiz/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _accountServices.Delete(id);
            return Ok(result);
        }
    }
}
