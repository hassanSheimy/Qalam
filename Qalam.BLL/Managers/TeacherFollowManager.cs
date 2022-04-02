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
    public class TeacherFollowManager : Repository<TeacherFollow>
    {
        private readonly IMapper _mapper;
        public TeacherFollowManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<bool> FollowTeacher(int studentId,int teacherId)
        {
            try
            {
                var obj = new TeacherFollow { StudentId = studentId, TeacherId = teacherId, FollowingDate = DateTime.UtcNow };
                var result = Add(obj);
                if (result == null || !SaveChanges())
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

        public Result<bool> IsFollow(FollowLessonViewModel FollowLessonViewModel)
        {
            try
            {
                var b = Get(s => s.StudentId == FollowLessonViewModel.StudentId
                            && s.Teacher.Timetables.Any(t => t.TimetableId == FollowLessonViewModel.TeacherTimetableId));

                if (b == null)
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
    }
}
