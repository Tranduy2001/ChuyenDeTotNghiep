using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Models;
using System.Linq.Expressions;
using static WebAppChamThiOl.Models.Constants;
using System.IO;
using WebAppChamThiOl.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Scripting.Utils;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ClosedXML.Excel;

namespace WebAppChamThiOl.Services
{
    public class ReportServices
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly CategoryServices _categoryServices;
        private readonly DataContext _dbContext;
        public ReportServices(CategoryServices categoryServices, IWebHostEnvironment hostEnvironment, DataContext dbContext)
        {
            _categoryServices = categoryServices;
            _hostEnvironment = hostEnvironment;
            _dbContext = dbContext;
        }
        public async Task<CustomFile> GetDataCategoryReport(int reportType)
        {
            if (reportType == 1)
            {
                var data = await _categoryServices.GetAll();
                if (data.Any())
                {
                    var result = data.Select((item, index) =>
                     new ReportCategoryViewModel()
                     {
                         Id = ++index,
                         Name = item.Name,
                         CreatedDate = item.CreatedDate,
                         Description = item.Description,
                         SUBJECT = item.SUBJECT
                     });
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string fileName = "BaoCaoDeThi.xlsx";

                    // Taken List of data from json file which we want to export to excel.
                    //List<Category> Category = new List<Category>();

                    using var workbook = new XLWorkbook();
                    IXLWorksheet worksheet = workbook.Worksheets.Add("BaoCaoDeThi");
                    worksheet.Cell(1, 1).Value = "STT";
                    worksheet.Column(1).Width = 8;
                    worksheet.Cell(1, 2).Value = "Tên môn học";
                    worksheet.Column(2).Width = 35;

                    worksheet.Cell(1, 3).Value = "Tên đề thi";
                    worksheet.Column(3).Width = 35;

                    worksheet.Cell(1, 4).Value = "Ngày tạo";
                    worksheet.Column(4).Width = 20;

                    worksheet.Cell(1, 5).Value = "Mô tả";
                    worksheet.Column(5).Width = 35;

                    worksheet.Cell(2, 1).InsertData(result);


                    var namesTable = worksheet.Range($"A1", $"E{result.Count() + 1}").CreateTable();
                    namesTable.Theme = XLTableTheme.TableStyleLight12;

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return new CustomFile()
                    {
                        ContentType = contentType,
                        FileContents = content,
                        FileName = fileName
                    };
                }
            }
            else
            {
                try
                {
                    var data = _dbContext.LOG_CHAM_THIS.ToList();
                    if (data.Any())
                    {
                        var result = data.Select((item, index) =>
                         new ReportStudentViewModel()
                         {
                             Id = ++index,
                             MaDe = item.MaDe,

                             TenThiSinh = item?.TenThiSinh,
                             DiemSo = item.Diem.GetValueOrDefault(0.0),
                             SoCauTraLoiDung = item.SoCauTraLoiDung,
                             SoLuongCauHoi = item.SoCauHoi,
                             ThoiGianThi = item.NgayChamThi.ToString()

                         });
                        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        string fileName = "BaoCaoThiSinh.xlsx";

                        // Taken List of data from json file which we want to export to excel.
                        //List<Category> Category = new List<Category>();

                        using var workbook = new XLWorkbook();
                        IXLWorksheet worksheet = workbook.Worksheets.Add("BaoCaoThiSinh");
                        worksheet.Cell(1, 1).Value = "STT";
                        worksheet.Column(1).Width = 8;
                        worksheet.Cell(1, 2).Value = "Mã đề";
                        worksheet.Column(2).Width = 35;

                        worksheet.Cell(1, 3).Value = "Tên thí sinh";
                        worksheet.Column(3).Width = 35;

                        worksheet.Cell(1, 4).Value = "Điểm";
                        worksheet.Column(4).Width = 20;

                        worksheet.Cell(1, 5).Value = "Số câu trả lời đúng";
                        worksheet.Column(5).Width = 20;

                        worksheet.Cell(1, 6).Value = "Số câu hỏi";
                        worksheet.Column(6).Width = 20;

                        worksheet.Cell(1, 7).Value = "Thời gian thi";
                        worksheet.Column(7).Width = 35;

                        worksheet.Cell(2, 1).InsertData(result);


                        var namesTable = worksheet.Range($"A1", $"G{result.Count() + 1}").CreateTable();
                        namesTable.Theme = XLTableTheme.TableStyleLight12;

                        using var stream = new MemoryStream();
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return new CustomFile()
                        {
                            ContentType = contentType,
                            FileContents = content,
                            FileName = fileName
                        };
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            return null;
        }
    }
}