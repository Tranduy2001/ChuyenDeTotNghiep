using WebAppChamThiOl.Data;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Models;
using WebAppChamThiOl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using static WebAppChamThiOl.Services.PythonScript;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;

namespace WebAppChamThiOl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ShortStoryServices _shortStoryServices;
        private readonly SubjectServices _subjectServices;
        private readonly QuizServices _quizServices;
        private readonly CategoryServices _categoryServices;
        private readonly ReportServices _reportServices;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, QuizServices quizServices, CategoryServices categoryServices, SubjectServices subjectServices, ReportServices reportServices, IWebHostEnvironment env)
        {
            _logger = logger;
            _quizServices = quizServices;
            _categoryServices = categoryServices;
            _subjectServices = subjectServices;
            _reportServices = reportServices;
            _env = env;
        }

        public async Task<IActionResult> Index(int? page = 1, int? subjectId = null)
        {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RunPython()
        {
            var py = new PythonScript();
            var path = @$"{_env.ContentRootPath}\Book_crawling";
            py.RunCmdPython(path);
            System.Threading.Thread.Sleep((int)System.TimeSpan.FromSeconds(3).TotalMilliseconds);

            var kq = py.GetDataRunCmd(path);
            var result = _categoryServices.ChamThi(kq);
            //ViewBag.SBD = result?.SBD;
            //ViewBag.Diem = result?.Diem;
            //ViewBag.SoCauTraLoiDung = result?.SoCauTraLoiDung;
            //ViewBag.SoLuongCauHoi = result?.SoLuongCauHoi;
            return View(result);
        }

        public IActionResult Result()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> DownloadExcelDocument(int? reportType = 1)
        {
            var data = await _reportServices.GetDataCategoryReport(reportType.Value);
            if (data != null)
            {
                return File(data.FileContents, data.ContentType, data.FileName);
            }
            return View();
        }
    }
}