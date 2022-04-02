using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.MYSQL.Repository;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class TeacherTimetableManager : Repository<TeacherTimetable>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<TimetableManager> _timetableManager;
        public TeacherTimetableManager(QalamDBContext dBContext, IMapper mapper, 
            Lazy<TimetableManager> timetableManager) : base(dBContext)
        {
            _mapper = mapper;
            _timetableManager = timetableManager;
        }

        public Result<int> AddTimetable(int teacherId, TimetableViewModel timetable)
        {
            try
            {
                if (!TimeSpan.TryParse(timetable.StartTime, out TimeSpan start))
                {
                    throw new Exception("Start time isn't valid");
                }

                var data = new TeacherTimetable
                {
                    TeacherId = teacherId,
                    TimetableId = timetable.Id,
                    Start = start
                };

                var result = Add(data);

                if(result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: result.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<PageResult<TimetableMinimalViewModel>> GetStudentFollowLive(int StudentId, int pageNo, int pageSize)
        {
            try
            {
                //var nextLessons = GetAllPaginated(n => n.Followers.Any(s => s.StudentId == StudentId) || n.Teacher.Followers.Any(s => s.StudentId == StudentId),
                //                pageSize, pageNo, "Timetable.Course.Subject", "Timetable.Course.EducationYear", "Teacher.User");
                
                //if (nextLessons.PageData == null)
                //{
                //    throw new Exception(EStatusCode.DatabaseError.Description());
                //}

                //var data = _mapper.Map<List<TimetableMinimalViewModel>>(nextLessons.PageData);
                //var pageResult = new PageResult<TimetableMinimalViewModel>
                //{
                //    Count = nextLessons.Count,
                //    PageData = data,
                //    PageNo = nextLessons.PageNo,
                //    PageSize = nextLessons.PageSize
                //};
                return ResultHelper.Succeeded(data: new PageResult<TimetableMinimalViewModel>());
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<TimetableMinimalViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<List<TimetableViewModel>> GetTimetable(int teacherId, int? courseId)
        {
            try
            {
                var teachertimeTables = GetAll(t => t.TeacherId == teacherId 
                    && t.Timetable.CourseId == (courseId ?? t.Timetable.CourseId),
                    new string[] { "Timetable.Course.EducationYear", "Timetable.Course.Subject" });

                if (teachertimeTables == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<TimetableViewModel>>(teachertimeTables);
                var ids = teachertimeTables.Select(t => t.TimetableId).ToList();

                var timeTables = _timetableManager.Value.GetAll(t => !ids.Contains(t.Id)
                    && ((courseId != null && t.CourseId == courseId)
                    || (courseId == null && t.Course.Teachers.Any(teacher => teacher.TeacherId == teacherId))),
                    new string[] { "Course.EducationYear", "Course.Subject" });

                if (timeTables == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                result.AddRange(_mapper.Map<List<TimetableViewModel>>(timeTables));

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<TimetableViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<LiveStreamViewModel> IsValidStreamTime(int teacherId)
        {
            try
            {
                var allTimes = GetAll(t => t.TeacherId == teacherId, new string[] { "Timetable" });

                if (allTimes == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var timetable = allTimes.FirstOrDefault(b => Math.Abs(b.Start.TotalMinutes - DateTime.UtcNow.TimeOfDay.TotalMinutes) <= 30);
                var result = _mapper.Map<LiveStreamViewModel>(timetable);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<LiveStreamViewModel>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
