using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppChamThiOl.Entities
{
    [Table("Subject")]
    public class SUBJECT
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên thể loại")]
        [Required(ErrorMessage = "Trường này không được để trống!")]
        public string Name { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Ngày kết thúc")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        public virtual ICollection<CATEGORY>? CATEGORIES { get; set; }

    }
}
