using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppChamThiOl.Models
{
    public class QuestionResultModel
    {
        public int QuizId { get; set; }
        public int QuizResultId { get; set; }
        public bool IsTrue { get; set; }
    }
}