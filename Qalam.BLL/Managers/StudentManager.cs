using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Qalam.Common.Helper;
using Qalam.BLL.ViewModels;
using System.Text.RegularExpressions;
using Qalam.Common.Enums;
using System.Net.Mail;
using Qalam.MYSQL.Repository;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class StudentManager : Repository<Student>
    {
        private readonly IMapper _mapper;

        public StudentManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<StudentPointViewModel> GetStudentPoints(int userId)
        {
            try
            {
                var points = Get(p => p.Id == userId);
                
                if (points == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var data = _mapper.Map<StudentPointViewModel>(points);

                return ResultHelper.Succeeded(data: data);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<StudentPointViewModel>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
            throw new NotImplementedException();
        }

    }
}
