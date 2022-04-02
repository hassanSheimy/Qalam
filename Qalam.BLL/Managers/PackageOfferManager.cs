using Qalam.MYSQL.Repository;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.Managers
{
    public class PackageOfferManager : Repository<PackageOffer>
    {
        public PackageOfferManager(QalamDBContext dBContext) : base(dBContext)
        {

        }


    }
}
