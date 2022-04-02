using AutoMapper;
using Qalam.BLL.Hubs;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using Qalam.MYSQL.Repository;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class StudentVideoManager : Repository<StudentVideo>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<StudentPackageManager> _studentPackageManager;
        private readonly Lazy<LiveStreamManager> _liveVideoManager;
        private readonly Lazy<LiveHandler> _liveHandler;
        public StudentVideoManager(QalamDBContext dBContext, IMapper mapper, 
            Lazy<StudentPackageManager> studentPackageManager, 
            Lazy<LiveStreamManager> liveVideoManager,
            Lazy<LiveHandler> liveHandler) : base(dBContext)
        {
            _mapper = mapper;
            _studentPackageManager = studentPackageManager;
            _liveVideoManager = liveVideoManager;
            _liveHandler = liveHandler;
        }

        public Result<bool> BookVideo(int lessonVideoId, int studentId)
        {
            try
            {
                var lesson = new StudentVideo
                {
                    LiveStreamId = lessonVideoId,
                    StudentId = studentId,
                    Rate = 0
                };
                var addResult = Add(lesson);

                if (addResult == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: true);
            }   
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<bool> DeletBook(int studentVideoId)
        {
            try
            {
                Delete(studentVideoId);
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

        public Result<bool> Review(int studentId,StudentVideoViewModel studentVideoViewModel)
        {
            try
            {
                studentVideoViewModel.StudentId = studentId;
                var obj = new StudentVideo
                {
                    Id = studentVideoViewModel.Id,
                    StudentId = studentVideoViewModel.StudentId,
                    Rate = studentVideoViewModel.Rate,
                    Comment = studentVideoViewModel.Comment
                };

                Update(obj);
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

        public Result<int?> Watch(int studentId, string streamKey)
        {
            try
            {
                //get LiveId from cache
                int? liveId = _liveHandler.Value.GetLiveId(streamKey);

                if(liveId == null)
                {
                    throw new Exception(EStatusCode.InValidStreamKey.Description());
                }

                var lesson = new StudentVideo
                {
                    LiveStreamId = (int)liveId,
                    StudentId = studentId,
                    Rate = 0,
                    ViewDate = DateTime.UtcNow
                };
                var addResult = Add(lesson);

                if (addResult == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded<int?>(data: addResult.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int?>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
            
        }
    }
}
