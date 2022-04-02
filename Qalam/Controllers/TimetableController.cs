using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TimetableController : ControllerBase
    {
        private readonly Lazy<TimetableManager> _manager;
        public TimetableController(Lazy<TimetableManager> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Authorization(EUserTypes.Admin)]
        public Result<List<TimetableViewModel>> GetTimetable([FromQuery] int? countryId, [FromQuery] int? educationTypeId, [FromQuery] int? educationYearId, [FromQuery] int? courseId)
        {
            return _manager.Value.GetAdminTimetable(countryId, educationTypeId, educationYearId, courseId);
        }

        [HttpPost]
        [Authorization(EUserTypes.Admin)]
        public Result<int> AddTimetable([FromBody] TimetableViewModel timetable)
        {
            return _manager.Value.AddTimetable(timetable);
        }

        [HttpPut]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> EditTimetable([FromBody] TimetableViewModel timetable)
        {
            return _manager.Value.EditTimetable(timetable);
        }

        [HttpDelete]
        [Route("{timetableId}")]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> DeleteTimetable([FromRoute] int timetableId)
        {
            return _manager.Value.DeleteTimetable(timetableId);
        }
    }
}
