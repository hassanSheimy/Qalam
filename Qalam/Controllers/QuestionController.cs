using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Extensions;
using Qalam.Common.Helper;
using Qalam.Configurations;
using System;
using System.Collections.Generic;
using System.IO;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly Lazy<QuestionManager> _manager;
        private readonly Lazy<RequestAttributes> _requestAttribute;
        public QuestionController(Lazy<QuestionManager> manager,
            Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _requestAttribute = requestAttribute;
        }

        [HttpPost]
        [Authorization(EUserTypes.Admin)]
        public Result<QuestionUploadResultViewModel> AddQuestions([FromQuery] int lessonId, [FromQuery] int languageId, [FromQuery] int typeId)
        {
            try
            {
                var httpRequest = HttpContext.Request.Form;

                if (httpRequest.Files.Count != 1)
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var file = httpRequest.Files[0];
                IList<string> allowedFileExtensions = new List<string> { ".csv" };
                string extension = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();

                if (!allowedFileExtensions.Contains(extension))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                var fileStream = new MemoryStream(fileData);

                return _manager.Value.AddQuestions(_requestAttribute.Value.UserId, lessonId, languageId, typeId, fileStream);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<QuestionUploadResultViewModel>(message: e.Message);
            }
        }
    }
}