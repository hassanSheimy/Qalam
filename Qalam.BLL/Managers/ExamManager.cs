using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.MYSQL.Repository;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class ExamManager : Repository<Exam>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<QuestionManager> _questionManager;
        public ExamManager(QalamDBContext dBContext, IMapper mapper, Lazy<QuestionManager> questionManager) : base(dBContext)
        {
            _mapper = mapper;
            _questionManager = questionManager;
        }

        public Result<PageResult<ExamViewModel>> GetTeacherExams(int userId, int? subjectId, int pageNo, int pageSize)
        {
            try
            {
                var exams = GetAllPaginated(e => e.SetterId == userId && e.CourseId == (subjectId ?? e.CourseId),
                            pageSize, pageNo, "Course.Subject");

                if (exams.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<ExamViewModel>>(exams.PageData);

                var pageResult = new PageResult<ExamViewModel>
                {
                    Count = exams.Count,
                    PageData = result,
                    PageNo = exams.PageNo,
                    PageSize = exams.PageSize
                };

                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<ExamViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<List<QuestionsTypeViewModel>> CreateExam(List<int> lessonsId)
        {
            var x = _questionManager.Value.CreateExam(lessonsId);

            throw new NotImplementedException();
        }
    }
}
