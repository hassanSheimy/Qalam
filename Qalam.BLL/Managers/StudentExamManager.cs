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
    public class StudentExamManager : Repository<StudentExam>
    {
        private readonly IMapper _mapper;
        public StudentExamManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<PageResult<StudentExamsDegreeModel>> GetStudentExams(int StudentId, int? subjectId, int pageNo, int pageSize)
        {
            try
            {
                var exams = GetAllPaginated(e => e.StudentId == StudentId && e.Exam.CourseId == (subjectId ?? e.Exam.CourseId),
                       pageSize, pageNo, "Exam.Cource.Subject", "Exam.Setter.User");

                if (exams.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<StudentExamsDegreeModel>>(exams.PageData);

                var pageResult = new PageResult<StudentExamsDegreeModel>
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
                return ResultHelper.Failed<PageResult<StudentExamsDegreeModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }
    }
}
