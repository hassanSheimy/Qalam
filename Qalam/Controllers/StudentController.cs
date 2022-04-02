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
    public class StudentController : ControllerBase 
    {
        private readonly Lazy<StudentManager> _manager;
        private readonly Lazy<StudentExamManager> _examManager;
        private readonly Lazy<StudentPackageManager> _studentPackageManager;
        private readonly Lazy<StudentVideoManager> _studentVideoManager;
        private readonly Lazy<TeacherTimetableManager> _teacherTimetableManager;
        private readonly Lazy<LiveStreamManager> _liveVideomanager;
        private readonly Lazy<RequestAttributes> _requestAttribute;

        public StudentController(Lazy<StudentManager> manager,
                                Lazy<StudentExamManager> examManager,
                                Lazy<StudentPackageManager> studentPackageManager,
                                Lazy<StudentVideoManager> managerVideoManager,
                                Lazy<TeacherTimetableManager> teacherTimetableManager,
                                Lazy<LiveStreamManager> liveVideomanager,
                                Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _examManager = examManager;
            _studentPackageManager = studentPackageManager;
            _studentVideoManager = managerVideoManager;
            _teacherTimetableManager = teacherTimetableManager;
            _liveVideomanager = liveVideomanager;
            _requestAttribute = requestAttribute;
        }
        /// <summary>
        /// Get the StudentPoint
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Points")]
        [Authorization(EUserTypes.Student)]
        public Result<StudentPointViewModel> GetStudentPoints ()
        {
            return _manager.Value.GetStudentPoints(_requestAttribute.Value.StudentId);
        }

        /// <summary>
        /// Get all LiveVideos that Student Watched(History)
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Watched-Lessons")]
        [Authorization(EUserTypes.Student)]
        public Result<PageResult<LessonVideoMinimalViewModel>> GetWatchedLessons([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _liveVideomanager.Value.GetWatchedLessons(_requestAttribute.Value.StudentId, pageNo, pageSize);
        }

        /// <summary>
        /// Get all LiveVideo that Student follow
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Comming-Lessons")]
        [Authorization(EUserTypes.Student)]
        public Result<PageResult<TimetableMinimalViewModel>> GetStudentFollowLive([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _teacherTimetableManager.Value.GetStudentFollowLive(_requestAttribute.Value.StudentId, pageNo, pageSize);
        }

        /// <summary>
        /// Get the Exams that student had entered
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Exams")]
        [Authorization(EUserTypes.Student)]
        public Result<PageResult<StudentExamsDegreeModel>> GetStudentExams([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _examManager.Value.GetStudentExams(_requestAttribute.Value.StudentId, 0, pageNo, pageSize);
        }

        /// <summary>
        /// Get the Packages that student had booked
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Packages")]
        [Authorization(EUserTypes.Student)]
        public Result<List<StudentPackageViewModel>> GetStudentPackages()
        {
            return _studentPackageManager.Value.GetStudentPackages(_requestAttribute.Value.StudentId);
        }
        
    }
}
