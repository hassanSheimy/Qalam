using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using Qalam.Configurations;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly Lazy<TeacherManager> _manager;
        private readonly Lazy<CourseManager> _courceManager;
        private readonly Lazy<TeacherCourseManager> _teacherCourseManager;
        private readonly Lazy<LiveStreamManager> _liveVideoManager;
        private readonly Lazy<TeacherFollowManager> _followTeacherManager;
        private readonly Lazy<TeacherTimetableManager> _teacherTimetableManager;
        private readonly Lazy<RequestAttributes> _requestAttribute;
        public TeacherController(Lazy<TeacherManager> manager, 
                                Lazy<TeacherCourseManager> teacherSubjectManager,
                                Lazy<CourseManager> courceManager, 
                                Lazy<LiveStreamManager> liveVideoManager,
                                Lazy<TeacherFollowManager> followTeachermanager,
                                Lazy<TeacherTimetableManager> teacherTimetableManager,
                                Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _teacherCourseManager = teacherSubjectManager;
            _courceManager = courceManager;
            _liveVideoManager = liveVideoManager;
            _followTeacherManager = followTeachermanager;
            _teacherTimetableManager = teacherTimetableManager;
            _requestAttribute = requestAttribute;
        }

        /// <summary>
        /// Listing All Teachers (that is confirmed) for students
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="educationTypeId"></param>
        /// <param name="educationYearId"></param>
        /// <param name="subjectId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Listing")]
        public Result<PageResult<TeacherViewModel>> GetTeachers([FromQuery] int? countryId, [FromQuery] int? educationTypeId, [FromQuery] int? educationYearId, [FromQuery] int? subjectId, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _manager.Value.GetTeachers(countryId, educationTypeId, educationYearId, subjectId, pageNo, pageSize);
        }

        /// <summary>
        /// Listing Teacher for admin
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="educationTypeId"></param>
        /// <param name="educationYearId"></param>
        /// <param name="subjectId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public Result<PageResult<TeacherInfoViewModel>> GetAllTeachers([FromQuery] int? countryId, [FromQuery] int? educationTypeId, [FromQuery] int? educationYearId, [FromQuery] int? subjectId, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _manager.Value.GetAllTeachers(countryId, educationTypeId, educationYearId, subjectId, pageNo, pageSize);
        }

        /// <summary>
        /// Listing Courses (Years) that Teacher teaches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Courses")]
        [Authorization(EUserTypes.Teacher)]
        public Result<List<TeacherCourseViewModel>> GetTeacherCourses()
        {
            return _teacherCourseManager.Value.GetTeacherCourses(_requestAttribute.Value.TeacherId);
        }

        /// <summary>
        /// Get all LiveVideos that Teacher explained
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Explanations")]
        [Authorization(EUserTypes.Teacher)]
        public Result<PageResult<LessonVideoInfoViewModel>> GetTeacherExplanations([FromQuery]int? EducationYearId, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _liveVideoManager.Value.GetTeacherExplanations(_requestAttribute.Value.TeacherId, EducationYearId, pageNo, pageSize);
        }

        /// <summary>
        /// Add A course of the same subject for teacher
        /// </summary>
        /// <param name="teacherCourse"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Course")]
        [Authorization(EUserTypes.Teacher)]
        public Result<int> AddCourse([FromBody] TeacherCourseViewModel teacherCourse)
        {
            return _teacherCourseManager.Value.AddCourse(_requestAttribute.Value.TeacherId, teacherCourse);
        }

        /// <summary>
        /// delete access for teacher to course
        /// </summary>
        /// <param name="teacherCourse"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Course")]
        [Authorization(EUserTypes.Teacher)]
        public Result<bool> DeletSubject([FromBody] TeacherCourseViewModel teacherCourse)
        {
            return _teacherCourseManager.Value.DeleteCourse(teacherCourse);
        }

        /// <summary>
        /// student request to follow teacher
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="TeacherId"></param>
        /// <param name="FollowingDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Follow/{teacherId}")]
        [Authorization(EUserTypes.Student)]
        public Result<bool> FollowTeacher([FromRoute] int teacherId)
        {
            return _followTeacherManager.Value.FollowTeacher(_requestAttribute.Value.StudentId, teacherId);
        }

        [HttpGet]
        [Route("Timetable")]
        [Authorization(EUserTypes.Teacher)]
        public Result<List<TimetableViewModel>> GetTeacherTimetable([FromQuery] int? courseId)
        {
            return _teacherTimetableManager.Value.GetTimetable(_requestAttribute.Value.TeacherId, courseId);
        }

        [HttpPost]
        [Route("Timetable")]
        [Authorization(EUserTypes.Teacher)]
        public Result<int> AddTeacherTimetable([FromBody] TimetableViewModel timetable)
        {
            return _teacherTimetableManager.Value.AddTimetable(_requestAttribute.Value.TeacherId, timetable);
        }
    }
}