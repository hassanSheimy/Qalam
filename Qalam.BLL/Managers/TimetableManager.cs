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
    public class TimetableManager : Repository<Timetable>
    {
        private readonly IMapper _mapper;

        public TimetableManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<int> AddTimetable(TimetableViewModel timetable)
        {
            try
            {
                if (!TimeSpan.TryParse(timetable.StartTime, out TimeSpan start)
                ||  !TimeSpan.TryParse(timetable.EndTime, out TimeSpan end)
                ||  !Enum.IsDefined(typeof(DayOfWeek), timetable.Day))
                {
                    throw new Exception("Time interval isn't valid");
                }

                var data = _mapper.Map<Timetable>(timetable);

                var result = Add(data);

                if (result == null || !SaveChanges())
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

        public Result<bool> DeleteTimetable(int timetableId)
        {
            try
            {
                Delete(timetableId);

                if(!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<bool> EditTimetable(TimetableViewModel timetableViewModel)
        {
            try
            {
                if (!TimeSpan.TryParse(timetableViewModel.StartTime, out TimeSpan start)
                || !TimeSpan.TryParse(timetableViewModel.EndTime, out TimeSpan end)
                || !Enum.IsDefined(typeof(DayOfWeek), timetableViewModel.Day))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var timetable = _mapper.Map<Timetable>(timetableViewModel);

                Update(timetable);
                if (!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<List<TimetableViewModel>> GetAdminTimetable(int? countryId, int? educationTypeId, int? educationYearId, int? courseId)
        {
            try
            {
                var timeTables = GetAll(t => t.CourseId == (courseId ?? t.CourseId)
                                    && !t.Course.ExcludedContents.Any(e => e.EducationTypeId == (educationTypeId ?? -1))
                                    && t.Course.EducationYearId == (educationYearId ?? t.Course.EducationYearId)
                                    && t.Course.Subject.CountryId == (countryId ?? t.Course.Subject.CountryId),
                                    new string[] { "Course.EducationYear.EducationType", "Course.Subject.Country" });

                if(timeTables == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<TimetableViewModel>>(timeTables);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<TimetableViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
