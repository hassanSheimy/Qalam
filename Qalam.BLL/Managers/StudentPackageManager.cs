using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.MYSQL.Repository;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class StudentPackageManager : Repository<StudentPackage>
    {
        private readonly IMapper _mapper;

        public StudentPackageManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<List<StudentPackageViewModel>> GetStudentPackages(int StudentId)
        {
            try
            {
                var packages = GetAll(l => l.StudentId == StudentId && (l.VideoCount != 0 || l.ExamCount != 0) 
                                      && l.ExpiryDate >= DateTime.UtcNow , "Package.Image");

                if (packages == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<StudentPackageViewModel>>(packages);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<StudentPackageViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<int> BookPackage(int studentId, int packageId)
        {
            throw new NotImplementedException();
        }
    }
}
