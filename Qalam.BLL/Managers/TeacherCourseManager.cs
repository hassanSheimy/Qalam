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
    public class TeacherCourseManager : Repository<TeacherCourse>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<TeacherTimetableManager> _teacherTimetableManager;
        public TeacherCourseManager(QalamDBContext dBContext, IMapper mapper,
            Lazy<TeacherTimetableManager> teacherTimetableManager) : base(dBContext)
        {
            _mapper = mapper;
            _teacherTimetableManager = teacherTimetableManager;
        }

        public Result<int> AddCourse(int teacherId, TeacherCourseViewModel teacherCourse)
        {
            try
            {
                teacherCourse.TeacherId = teacherId;
                teacherCourse.Id = 0;
                var subject = _mapper.Map<TeacherCourse>(teacherCourse);

                var result = Add(subject);

                if (result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: result.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<bool> DeleteCourse(TeacherCourseViewModel teacherCourse)
        {
            try
            {
                Delete(teacherCourse.Id);

                var timetableResult = _teacherTimetableManager.Value.GetAll(t => t.TeacherId == teacherCourse.TeacherId 
                                        && t.Timetable.CourseId == teacherCourse.CourseId);

                if (timetableResult == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                _teacherTimetableManager.Value.Delete(timetableResult.Select(t => t.Id).ToArray());

                if(!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<List<TeacherCourseViewModel>> GetTeacherCourses(int teacherId)
        {
            try
            {
                var x = GetAll(y => y.TeacherId == teacherId, new string[] { "Course.EducationYear" });
                if (x == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }
                var years = _mapper.Map<List<TeacherCourseViewModel>>(x);

                return ResultHelper.Succeeded(data: years);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<TeacherCourseViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }
    }
}
