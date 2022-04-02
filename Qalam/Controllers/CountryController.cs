using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class CountryController : ControllerBase
    {
        private readonly Lazy<CountryManager> _manager;

        public CountryController(Lazy<CountryManager> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorization(EUserTypes.Admin)]
        public Result<List<CountryViewModel>> GetCountries()
        {
            return _manager.Value.GetCountries();
        }

        /// <summary>
        /// Add country with education types, subject, years and courses
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorization(EUserTypes.Admin)]
        public Result<int> AddCountry([FromBody] CountryViewModel country)
        {
            return _manager.Value.AddCountry(country);
        }

        [HttpPut]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> EditCountry([FromBody] CountryViewModel country)
        {
            return _manager.Value.EditCountry(country);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorization(EUserTypes.Admin)]
        public Result<bool> DeleteCountry([FromRoute] int id)
        {
            return _manager.Value.DeleteCountry(id);
        }
    }
}