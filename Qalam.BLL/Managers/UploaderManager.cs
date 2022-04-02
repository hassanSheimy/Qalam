using AutoMapper;
using Qalam.Common.Enums;
using Qalam.MYSQL.Repository;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class UploaderManager : Repository<DataFile>
    {
        private readonly IMapper _mapper;
        public UploaderManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<List<int>> AddFiles(List<DataFile> files)
        {
            try
            {
                var result = Add(files);

                if(result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded<List<int>>(data: null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
