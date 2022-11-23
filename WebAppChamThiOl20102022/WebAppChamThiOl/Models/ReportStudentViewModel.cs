using ClosedXML.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppChamThiOl.Entities;

namespace WebAppChamThiOl.Models
{
    public class ReportStudentViewModel
    {
        public int Id { get; set; }
        public string MaDe { get; set; }

        [Display(Name = "Tên thí sinh")]
        public string TenThiSinh { get; set; }
        public double DiemSo { get; set; }
        public int SoCauTraLoiDung { get; set; }
        public int SoLuongCauHoi { get; set; }
        public string ThoiGianThi { get; set; }
    }
}
