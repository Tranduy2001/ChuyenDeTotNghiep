using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAppChamThiOl.Data;
using WebAppChamThiOl.Models;

namespace WebAppChamThiOl.Entities
{
    [Table("LogChamThi")]
    public class LOG_CHAM_THI
    {
        [Key]
        public int Id { get; set; }
        public DateTime NgayChamThi { get; set; }
        public double? Diem { get;set; }
        public string SBD { get;set; }
        public int SoCauTraLoiDung { get; set; }
        public int SoCauHoi { get; set; }
        public string TenThiSinh { get; set; }
        public string MaDe { get; set; }
    }
}