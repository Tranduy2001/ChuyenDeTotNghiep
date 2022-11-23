using ClosedXML.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppChamThiOl.Entities;

namespace WebAppChamThiOl.Models
{
    public class ReportCategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên đề")]
        public string Name { get; set; }
        public string SubjectName
        {
            get
            {
                return this.SUBJECT?.Name ?? "";
            }
        }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "Môn học")]
        [ForeignKey("SubjectId")]
        public int? SubjectId { get; set; }
        [XLColumn(Ignore = true)]
        public virtual SUBJECT? SUBJECT { get; set; }
        //[XLColumn(Ignore = true)]
       
    }
}
