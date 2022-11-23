using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppChamThiOl.Entities;
using WebAppChamThiOl.Models;
using System.Linq.Expressions;
using static WebAppChamThiOl.Models.Constants;
using WebAppChamThiOl.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAppChamThiOl.Services
{
    public class ResultQuizServices
    {
        private readonly DataContext dbContext;
        public ResultQuizServices(DataContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public List<RESULT_QUIZ> GetDapAnByCauHoiId(int id)
        {
            return dbContext.RESULT_QUIZ.Include(x => x.QUIZ).Where(x => x.QuizId == id).ToList();
        }
        public async Task<RESULT_QUIZ> Get(int? id)
        {
            if (!id.HasValue) return null;
            var data = await dbContext.RESULT_QUIZ.FindAsync(id);
            return data;
        }

        public void Add(RESULT_QUIZ model)
        {
            dbContext.RESULT_QUIZ.Add(model);
            dbContext.SaveChanges();
        }

        public void Update(RESULT_QUIZ model)
        {
            dbContext.Entry(model).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        public RESULT_QUIZ GetById(int id)
        {
            return dbContext.RESULT_QUIZ.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                RESULT_QUIZ dE = dbContext.RESULT_QUIZ.Find(id);
                dbContext.RESULT_QUIZ.Remove(dE);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<QUIZ> GetAllCauHoi()
        {
            return dbContext.QUIZ.ToList();
        }
    }
}