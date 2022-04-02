using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Extensions;
using Qalam.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.Managers
{
    public class SystemManager
    {
        private readonly Lazy<CountryManager> _countryManager;
        private readonly Lazy<LanguageManager> _languageManager;

        public SystemManager(Lazy<CountryManager> countryManager, Lazy<LanguageManager> languageManager)
        {
            _countryManager = countryManager;
            _languageManager = languageManager;
        }

        public Result<InitSystemViewModel> Init()
        {
            try
            {
                var countries = _countryManager.Value.GetActiveCountries();

                if (!countries.IsSucceeded || countries.Data == null)
                {
                    throw new Exception(countries.Message);
                }

                var languages = _languageManager.Value.GetLanguages();

                if (!languages.IsSucceeded || languages.Data == null)
                {
                    throw new Exception(languages.Message);
                }

                var result = new InitSystemViewModel
                {
                    Countries = countries.Data,
                    Languages = languages.Data
                };

                return ResultHelper.Succeeded(result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<InitSystemViewModel>(message: e.Message);
            }
        }
    }
}
