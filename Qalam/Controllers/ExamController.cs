using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Helper;
using Qalam.Configurations;
using Qalam.Common.Enums;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly Lazy<ExamManager> _manager;
        private readonly Lazy<StudentExamManager> _studentExamManager;
        public ExamController(Lazy<ExamManager> manager, Lazy<StudentExamManager> studentExamManager)
        {
            _manager = manager;
            _studentExamManager = studentExamManager;
        }

        [HttpPost]
        [Route("CreateExam")]
        public Result<List<QuestionsTypeViewModel>> GetQuestions([FromBody] List<int> lessonsID)
        {
            return _manager.Value.CreateExam(lessonsID);
        }
    }
}