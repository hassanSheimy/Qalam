using AutoMapper;
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
    public class PackageManager : Repository<Package>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<PackageItemManager> _packageItemManager;
        private readonly Lazy<PackageOfferManager> _packageOfferManager;

        public PackageManager(QalamDBContext dBContext, IMapper mapper, 
            Lazy<PackageItemManager> packageItemManager, 
            Lazy<PackageOfferManager> packageOfferManager) : base(dBContext)
        {
            _mapper = mapper;
            _packageItemManager = packageItemManager;
            _packageOfferManager = packageOfferManager;
        }

        public Result<List<PackageViewModel>> PackageListing(int countryId)
        {
            try
            {
                var packages = GetAll(p => p.IsActive && p.IsVisible && p.Items.Any(i => i.IsActive && i.IsVisible), 
                    "Image", "Items.Offers", "Items.Prices.Country");

                if(packages == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                foreach(var package in packages)
                {
                    package.Items = package.Items.Where(i => i.IsActive == true && i.IsVisible).ToList();
                }

                var result = _mapper.Map<List<PackageViewModel>>(packages, opt => opt.Items["countryId"] = countryId);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<PackageViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<int> AddPackage(PackageViewModel packageViewModel)
        {
            try
            {
                var package = _mapper.Map<Package>(packageViewModel);

                var result = Add(package);

                if (result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: result.Id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int>(statusCode: EStatusCode.InternalServerError, message: e.Message);
            }
        }

        public Result<bool> UpdatePackage(PackageViewModel packageViewModel)
        {
            try
            {
                //var deletedItems = packageViewModel.Items.Where(i => i.IsDeleted).Select(i => i.Id).ToArray();
                //var deletedOffers = packageViewModel.Offers.Where(o => o.IsDeleted).Select(o => o.Id).ToArray();
                //packageViewModel.Items.RemoveAll(i => i.IsDeleted);
                //packageViewModel.Offers.RemoveAll(o => o.IsDeleted);

                //_packageItemManager.Value.Delete(deletedItems);
                //_packageOfferManager.Value.Delete(deletedOffers);
                
                //var package = _mapper.Map<Package>(packageViewModel);
                //Update(package);
                //if (!SaveChanges())
                //{
                //    throw new Exception(EStatusCode.DatabaseError.Description());
                //}

                return ResultHelper.Succeeded(true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<bool> DeletePackage(int id)
        {
            try
            {
                Delete(id);

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

        public Result<List<PackageViewModel>> GetAllPackages()
        {
            try
            {
                var packages = GetAll(p => true, new string[] { "Students", "Items", "Offers", "Image" });

                if (packages == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<PackageViewModel>>(packages);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<PackageViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
