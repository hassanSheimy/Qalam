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
    public class TeacherManager : Repository<Teacher>
    {
        private readonly IMapper _mapper;
        public TeacherManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<PageResult<TeacherViewModel>> GetTeachers(int? countryId, int? educationTypeId, int? educationYearId, int? subjectId, int pageNo, int pageSize)
        {
            try
            {
                var teachers = GetAllPaginated(t=>t.User.IsConfirmed == true
                                            && t.SubjectId == (subjectId ?? t.SubjectId)
                                            && t.Courses.Any(y => y.Course.EducationYearId == (educationYearId ?? y.Course.EducationYearId))
                                            && t.Courses.Any(c => !c.Course.ExcludedContents.Any(e => e.EducationTypeId == (educationTypeId ?? -1)))
                                            && t.Subject.CountryId == (countryId ?? t.Subject.CountryId),
                                            pageSize, pageNo,
                                            "User.Files", "Courses.Course.EducationYear", "User.EducationType.Country", "Subject");

                if (teachers.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<TeacherViewModel>>(teachers.PageData);

                var pageResult = new PageResult<TeacherViewModel>
                {
                    Count = teachers.Count,
                    PageData = result,
                    PageNo = teachers.PageNo,
                    PageSize = teachers.PageSize
                };

                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<TeacherViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<PageResult<TeacherInfoViewModel>> GetAllTeachers(int? countryId, int? educationTypeId, int? educationYearId, int? subjectId, int pageNo, int pageSize)
        {
            try
            {
                var teachers = GetAllPaginated(t => t.SubjectId == (subjectId ?? t.SubjectId)
                                            && t.Courses.Any(y => y.Course.EducationYearId == (educationYearId ?? y.Course.EducationYearId))
                                            && t.Courses.Any(c => !c.Course.ExcludedContents.Any(e => e.EducationTypeId == (educationTypeId ?? -1)))
                                            && t.Subject.CountryId == (countryId ?? t.Subject.CountryId),
                                            pageSize, pageNo,
                                            "User.Files", "Courses.Course.EducationYear", "User.EducationType.Country", "Subject");

                if (teachers.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<TeacherInfoViewModel>>(teachers.PageData);

                var pageResult = new PageResult<TeacherInfoViewModel>
                {
                    Count = teachers.Count,
                    PageData = result,
                    PageNo = teachers.PageNo,
                    PageSize = teachers.PageSize
                };

                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<TeacherInfoViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }
    }
}
