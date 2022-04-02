using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly Lazy<UploaderManager> _manager;

        public UploaderController(Lazy<UploaderManager> manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("Image")]
        public Result<List<int>> Post([FromForm] IFormFile x)
        {
            try { 
                var httpRequest = HttpContext.Request.Form;
                List<DataFile> files = new List<DataFile>();
                foreach (var file in httpRequest.Files)
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)file.Length);
                    }
                    files.Add(new DataFile
                    {
                        Type = EFileTypes.Image,
                        FileName = file.FileName,
                        FilePath = @"data:image / jpeg; base64," + Convert.ToBase64String(fileData),
                        UploadDate = DateTime.UtcNow
                    });
                }

                return _manager.Value.AddFiles(files);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<int>>(message: e.Message);
            }
        }

        [HttpPost]
        [Route("ProfileImage")]
        public Result<List<int>> Posts()
        {
            try
            {
                var httpRequest = HttpContext.Request.Form;
                List<DataFile> files = new List<DataFile>();
                foreach (var file in httpRequest.Files)
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        fileData = binaryReader.ReadBytes((int)file.Length);
                    }
                    files.Add(new DataFile
                    {
                        Type = EFileTypes.Image,
                        FileName = file.FileName,
                        FilePath = @"data:image / jpeg; base64," + Convert.ToBase64String(fileData),
                        UploadDate = DateTime.UtcNow
                    });
                }

                return _manager.Value.AddFiles(files);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<int>>(message: e.Message);
            }
        }
    }
}