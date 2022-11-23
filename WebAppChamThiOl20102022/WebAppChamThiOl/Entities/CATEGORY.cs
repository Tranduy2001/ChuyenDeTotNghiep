using ClosedXML.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebAppChamThiOl.Entities
{
    [Table("Category")]
    public class CATEGORY
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống!")]
        [StringLength(500, ErrorMessage = "{0} từ {2} đến {1} ký tự", MinimumLength = 3)]
        [Display(Name = "Tên đề")]
        public string Name { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "Môn học")]
        [ForeignKey("SubjectId")]
        public int? SubjectId { get; set; }
        [XLColumn(Ignore = true)]
        public virtual SUBJECT? SUBJECT { get; set; }
        [XLColumn(Ignore = true)]

        public virtual ICollection<QUIZ>? QUIZS { get; set; }

    }
}