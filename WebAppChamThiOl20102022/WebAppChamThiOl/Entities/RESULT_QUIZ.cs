using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppChamThiOl.Entities
{
    [Table("ResultQuiz")]
    public class RESULT_QUIZ
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên câu hỏi")]
        public int? QuizId { get; set; }
        [Display(Name = "Nội dung đáp án")]
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string Name { get; set; }
        [Display(Name = "Số thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Là đáp án đúng")]
        public bool IsResultTrue { get; set; }

        [ForeignKey("QuizId")]
        public virtual QUIZ? QUIZ { get; set; }
    }
}