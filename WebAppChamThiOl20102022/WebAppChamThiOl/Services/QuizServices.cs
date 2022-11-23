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

namespace WebAppChamThiOl.Services
{
    public class QuizServices
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly DataContext dbContext;
        public QuizServices(DataContext _dbContext, IWebHostEnvironment hostEnvironment)
        {
            dbContext = _dbContext;
            _hostEnvironment = hostEnvironment;
        }
        
        public async Task<PagingResult<QUIZ>> GetAll(int page = 1, int pageSize = 10, string keyword = null, int? categoryId = null)
        {
            IQueryable<QUIZ> data = dbContext.QUIZ.Include(x => x.CATEGORY).OrderByDescending(x => x.Id);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(x => x.Name.Contains(keyword));
            }
            if (categoryId.HasValue)
            {
                data = data.Where(x => x.CategoryId == categoryId);
            }
            return await PagingResult<QUIZ>.CreateAsync(data, page, pageSize);
        }


        public async Task<QUIZ> Get(int? id)
        {
            if (!id.HasValue) return null;
            var data = await dbContext.QUIZ.FindAsync(id);
            return data;
        }

        public void Add(QUIZ model)
        {
            if (model.File != null)
            {
                var fileName = Path.GetFileName(model.File.FileName);

                var uploads = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
                var filePath = Path.Combine(uploads, fileName);
                model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                model.Image = $"/UploadedFiles/{fileName}";
            }

            dbContext.QUIZ.Add(model);
            dbContext.SaveChanges();
        }

        public void Update(QUIZ model)
        {
            if (model.File != null)
            {
                var fileName = Path.GetFileName(model.File.FileName);

                var uploads = Path.Combine(_hostEnvironment.WebRootPath, "UploadedFiles");
                var filePath = Path.Combine(uploads, fileName);
                model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                model.Image = $"/UploadedFiles/{fileName}";
            }

            var quiz = GetById(model.Id);
            if (quiz != null)
            {
                dbContext.QUIZ.Attach(quiz);
                if (model.File != null)
                {
                    quiz.Image = model.Image;
                }
                quiz.Name = model.Name;
                quiz.QuizType = model.QuizType;
                quiz.Description = model.Description;

                dbContext.Entry(quiz).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
        public QUIZ GetById(int id)
        {
            return dbContext.QUIZ.Include(x => x.CATEGORY).Include(x => x.RESULT_QUIZS).FirstOrDefault(x => x.Id == id);
        }
        public bool Delete(int id)
        {
            try
            {
                QUIZ dE = dbContext.QUIZ.Find(id);
                dbContext.QUIZ.Remove(dE);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SelectItem> GetListQuizType()
        {
            var data = new List<SelectItem>();
            var values = Enum.GetValues(typeof(QuizTypeEnum)).Cast<QuizTypeEnum>();
            int index = 1;
            foreach (var item in values)
            {
                data.Add(new SelectItem()
                {
                    Text = item.GetDisplayName(),
                    Value = (index++)
                });
            }
            return data;
        }
    }
}