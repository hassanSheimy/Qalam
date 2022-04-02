using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Helper;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly Lazy<SystemManager> _manager;
        public SystemController(Lazy<SystemManager> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("Init")]
        public Result<InitSystemViewModel> InitSystem()
        {
            return _manager.Value.Init();
        }
    }
}
