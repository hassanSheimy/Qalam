using Qalam.BLL.ViewModels;
using Qalam.MYSQL.Repository;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Qalam.BLL.Configurations;
using Qalam.Common.Enums;
using AutoMapper;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class QuestionManager: Repository<Question>
    {
        private readonly IMapper _mapper;
        private readonly CsvManager _csvManager;

        public QuestionManager(QalamDBContext dbContext, IMapper mapper, CsvManager csvManager) : base(dbContext)
        {
            _mapper = mapper;
            _csvManager = csvManager;
        }

        public Result<QuestionUploadResultViewModel> AddQuestions(int userId, int lessonId, int languageId, int typeId, MemoryStream file)
        {
            try
            {
                var rows = _csvManager.ReadFromFile<QuestionViewModel, QuestionsMapper>(file);

                if (rows == null || rows.Count == 0)
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var questions = new List<Question>();
                var result = new QuestionUploadResultViewModel
                {
                    NumberOfFaildRows = 0,
                    NumberOfSuccessRows = 0,
                    FaildRows = new List<int>()
                };
                var index = 0;

                foreach (var row in rows)
                {
                    if (IsValid(row, typeId))
                    {
                        var tempQuestion = _mapper.Map<Question>(row);
                        tempQuestion.LessonId = lessonId;
                        tempQuestion.SetterId = userId;
                        tempQuestion.Type = (EQuestionTypes)typeId;

                        questions.Add(tempQuestion);
                        result.NumberOfSuccessRows++;
                    }
                    else
                    {
                        result.NumberOfFaildRows++;
                        result.FaildRows.Add(index);
                    }
                    index++;
                }

                var addResult = Add(questions);

                if (addResult == null || !SaveChanges())
                { 
                    throw new Exception(EStatusCode.DatabaseError.Description()); 
                }

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<QuestionUploadResultViewModel>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        private bool IsValid(QuestionViewModel row, int questionType)
        {
            if(questionType == (int)EQuestionTypes.MultiplyChoose)
            {
                if(row.Answers.Count != 4 && row.DifficultyLevel < 1 && row.DifficultyLevel > 5 && row.Text == null)
                {
                    return false;
                }
                return true;
            }
            if (questionType == (int)EQuestionTypes.TrueOrFalse)
            {
                if (row.Answers.Count != 1 && row.DifficultyLevel < 1 && row.DifficultyLevel > 5 && row.Text == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        } 

        public List<QuestionsTypeViewModel> CreateExam(List<int> lessonsID)
        {
            try
            {
                //var all = GetAll(q => lessonsID.Contains(q.LessonId) , "Answers", "Type");
                //var x = all.GroupBy(q => q.TypeId).Select(q => new QuestionType {Id = q.Key , Name = q.First().Type.Name , Questions = q.ToList() }).ToList();
                //var result = _mapper.Map<List<QuestionsTypeViewModel>>(x);
                
                //for (int i=0; i < result.Count ;i++)
                //    result[i].Questions = result[i].Questions.OrderBy(x => Guid.NewGuid()).Take(2).ToList();

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
