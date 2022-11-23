using WebAppChamThiOl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppChamThiOl.Entities
{
    [Table("Quiz")]
    public class QUIZ
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên câu hỏi")]
        [StringLength(500, ErrorMessage = "{0} từ {2} đến {1} ký tự", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string Name { get; set; }
        //[Display(Name = "Đáp án đúng")]
        //[Required(ErrorMessage = "{0} không được để trống!")]
        //public int DapAnDung { get; set; }
        [Display(Name = "Thể loại")]
        [Required(ErrorMessage = "{0} không được để trống!")]
        public int? CategoryId { get; set; }
        [Display(Name = "Mô tả thêm")]
        public string? Description { get; set; }
        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        [Display(Name = "Loại câu hỏi")]
        public QuizTypeEnum? QuizType { get; set; } = QuizTypeEnum.QuizTest;
        [Display(Name = "Thể loại")]
        [ForeignKey("CategoryId")]
        public virtual CATEGORY? CATEGORY { get; set; }
        public virtual ICollection<RESULT_QUIZ>? RESULT_QUIZS { get; set; }
        [NotMapped]
        public int? IsSeleted { get; set; }
    }
}