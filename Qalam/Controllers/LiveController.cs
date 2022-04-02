using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class LiveController : ControllerBase
    {
        private readonly Lazy<LiveStreamManager> _manager;
        private readonly Lazy<StudentVideoManager> _studentVideoManager;
        private readonly Lazy<TeacherTimetableManager> _teacherTimetableManager;
        private readonly Lazy<RequestAttributes> _requestAttribute;
        public LiveController(Lazy<LiveStreamManager> manager, 
            Lazy<StudentVideoManager> studentVideoManager, 
            Lazy<TeacherTimetableManager> teacherTimetableManager, 
            Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _studentVideoManager = studentVideoManager;
            _teacherTimetableManager = teacherTimetableManager;
            _requestAttribute = requestAttribute;
        }

        /// <summary>
        /// Get all next LiveVideos based on timeTable of teachers 
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="educationTypeId"></param>
        /// <param name="educationYearId"></param>
        /// <param name="subjectId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Listing")]
        [Authorization(EUserTypes.Student, EUserTypes.Guest)]
        public Result<PageResult<LiveStreamViewModel>> GetLessonsVideo([FromQuery] int? countryId, [FromQuery] int? educationTypeId, [FromQuery] int? educationYearId, [FromQuery] int? subjectId, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _manager.Value.GetLive(_requestAttribute.Value.StudentId, countryId, educationTypeId, educationYearId, subjectId, pageNo, pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveStreamViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(EUserTypes.Teacher)]
        public Result<int> AddLessonVideo([FromBody] LiveStreamViewModel liveStreamViewModel)
        {
            return _manager.Value.AddLessonVideo(_requestAttribute.Value.TeacherId, liveStreamViewModel);
        }

        /// <summary>
        /// student request to follow lesson
        /// </summary>
        /// <param name="TeacherTimetableId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Follow/{TeacherTimetableId}")]
        [Authorization(EUserTypes.Student)]
        public Result<int> FollowLesson([FromRoute] int teacherTimetableId)
        {
            return null; //_followLmanager.Value.FollowLive(_requestAttribute.Value.StudentId, TeacherTimetableId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TeacherTimetableId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Follow/{id}")]
        [Authorization(EUserTypes.Student)]
        public Result<bool> DeletBook([FromRoute] int id)
        {
            return null; //_followLmanager.Value.DeletFollow(id);
        }

        /// <summary>
        /// check if it is available for teacher to stream a video
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Checktime")]
        [Authorization(EUserTypes.Teacher)]
        public Result<LiveStreamViewModel> IsValidStreamTime()
        {
            return _teacherTimetableManager.Value.IsValidStreamTime(_requestAttribute.Value.TeacherId);
        }

        /// <summary>
        /// student request to watch a live
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Watch/{streamKey}")]
        //[Authorization(EUserTypes.Student)]
        public Result<int?> Watch([FromRoute]string streamKey)
        {
            return _studentVideoManager.Value.Watch(_requestAttribute.Value.StudentId, streamKey);
        }

        [HttpPut]
        [Route("Review/{Id}")]
        [Authorization(EUserTypes.Student)]
        public Result<bool> Review([FromBody]StudentVideoViewModel studentVideoViewModel)
        {
            return _studentVideoManager.Value.Review(_requestAttribute.Value.StudentId, studentVideoViewModel);
        }
    }
}