using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using Qalam.MYSQL.Repository;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class LiveStreamManager : Repository<LiveStream>
    {
        private readonly IMapper _mapper;
        private readonly NotificationManager _notificationManager;
        public LiveStreamManager(QalamDBContext dBContext, IMapper mapper, NotificationManager notificationManager) : base(dBContext)
        {
            _mapper = mapper;
            _notificationManager = notificationManager;
        }
        
        public Result<PageResult<LiveStreamViewModel>> GetLive(int? studentId, int? countryId, int? educationTypeId, int? educationYearId, int? subjectId, int pageNo, int pageSize)
        {
            try
            {
                var live = GetAllPaginated(l => l.Course.EducationYear.CountryId == (countryId ?? l.Course.EducationYear.CountryId)
                                      && !l.Course.ExcludedContents.Any(e => e.EducationTypeId == (educationTypeId ?? e.EducationTypeId))
                                      && l.Course.EducationYearId == (educationYearId ?? l.Course.EducationYearId)
                                      && l.Course.SubjectId == (subjectId ?? l.Course.SubjectId), pageNo, pageSize,
                                      "Course.EducationYear.Country", "Course.Subject", "Teacher.User");

                if (live.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<LiveStreamViewModel>>(live.PageData, opt => opt.Items["studentId"] = studentId);

                result = result.OrderBy(l => l.ShowDate).ToList();

                var pageResult = new PageResult<LiveStreamViewModel>
                {
                    Count = live.Count,
                    PageData = result,
                    PageNo = live.PageNo,
                    PageSize = live.PageSize
                };

                return ResultHelper.Succeeded(pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<LiveStreamViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<PageResult<LessonVideoInfoViewModel>> GetTeacherExplanations(int teacherId, int? EducationYearId, int pageNo, int pageSize)
        {
            try
            {
                var lessons = GetAllPaginated(l => l.TeacherId == teacherId
                                              && l.Course.EducationYearId == (EducationYearId ?? l.Course.EducationYearId),
                                              pageSize, pageNo, "Students", "Course.EducationYear" );
                if (lessons.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<LessonVideoInfoViewModel>>(lessons.PageData);

                var pageResult = new PageResult<LessonVideoInfoViewModel>
                {
                    Count = lessons.Count,
                    PageData = result,
                    PageNo = lessons.PageNo,
                    PageSize = lessons.PageSize
                };

                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<LessonVideoInfoViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<int> AddLessonVideo(int teacherId, LiveStreamViewModel liveStreamViewModel)
        {
            try
            {
                liveStreamViewModel.TeacherId = teacherId;
                var lesson = _mapper.Map<LiveStream>(liveStreamViewModel);
                lesson.ShowDate = DateTime.UtcNow;

                var result = Add(lesson);

                if (result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                Task.Run(() => _notificationManager.SendLiveStreamNotifications(liveStreamViewModel));
                
                return ResultHelper.Succeeded(data: result.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<PageResult<LessonVideoMinimalViewModel>> GetWatchedLessons(int StudentId, int pageNo, int pageSize)
        {
            try
            {
                var lessons = GetAllPaginated(l => l.Students.Any(s => s.StudentId == StudentId && s.ViewDate < DateTime.UtcNow),
                    pageSize, pageNo, "Course.Subject", "Course.EducationYear", "Teacher.User");

                if (lessons.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }
                var data = _mapper.Map<List<LessonVideoMinimalViewModel>>(lessons.PageData);
                var pageResult = new PageResult<LessonVideoMinimalViewModel>
                {
                    Count = lessons.Count,
                    PageData = data,
                    PageNo = lessons.PageNo,
                    PageSize = lessons.PageSize
                };
                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<LessonVideoMinimalViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<PageResult<LessonVideoMinimalViewModel>> GetNextLessons(int userID, int pageNo, int pageSize)
        {
            try
            {
                var lessons = GetAllPaginated(l => l.Students.Any(s => s.StudentId == userID && s.ViewDate > DateTime.UtcNow),
                    pageSize, pageNo, "Lesson.Subject", "Lesson.Unit", "Teacher.User");
                if (lessons.PageData == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }
                var data = _mapper.Map<List<LessonVideoMinimalViewModel>>(lessons.PageData);
                var pageResult = new PageResult<LessonVideoMinimalViewModel>
                {
                    Count = lessons.Count,
                    PageData = data,
                    PageNo = lessons.PageNo,
                    PageSize = lessons.PageSize
                };
                return ResultHelper.Succeeded(data: pageResult);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<PageResult<LessonVideoMinimalViewModel>>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<int?> GetRecentlyLive(string streamKey)
        {
            try
            {
                var live = GetAll(l => l.Teacher.StreamKey == streamKey);
                
                if(live == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var data = live.Last();

                return ResultHelper.Succeeded(data: data?.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int?>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
