using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using Qalam.Configurations;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly Lazy<PackageManager> _manager;
        private readonly Lazy<StudentPackageManager> _studentPackageManager;
        private readonly Lazy<RequestAttributes> _requestAttribute;
        public PackageController(Lazy<PackageManager> manager, 
            Lazy<StudentPackageManager> studentPackageManager,
            Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _studentPackageManager = studentPackageManager;
            _requestAttribute = requestAttribute;
        }

        /// <summary>
        /// Get Active Packages for Listing view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Listing")]
        public Result<List<PackageViewModel>> GetActivePackages()
        {
            return _manager.Value.PackageListing(_requestAttribute.Value.CountryId);
        }

        /// <summary>
        /// Get all packages (active and non-active) for admin page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(EUserTypes.Admin)]
        public Result<List<PackageViewModel>> GetAllPackages()
        {
            return _manager.Value.GetAllPackages();
        }

        [HttpPost]
        [Authorization(EUserTypes.Admin)]
        public Result<int> AddPackage([FromBody] PackageViewModel package)
        {
            return _manager.Value.AddPackage(package);
        }

        [HttpPut]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> UpdatePackage([FromBody] PackageViewModel package)
        {
            return _manager.Value.UpdatePackage(package);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> DeletePackage([FromRoute] int id)
        {
            return _manager.Value.DeletePackage(id);
        }

        /// <summary>
        /// book a package for student
        /// </summary>
        /// <param name="studentPackageViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Book/{packageId}")]
        [Authorization(EUserTypes.Student)]
        public Result<int> BookPackage([FromRoute] int packageId)
        {
            return _studentPackageManager.Value.BookPackage(_requestAttribute.Value.StudentId, packageId);
        }
    }
}