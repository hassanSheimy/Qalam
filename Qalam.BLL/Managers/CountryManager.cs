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
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class CountryManager : Repository<Country>
    {
        private readonly IMapper _mapper;

        public CountryManager(QalamDBContext dBContext, IMapper mapper) : base(dBContext)
        {
            _mapper = mapper;
        }

        public Result<int> AddCountry(CountryViewModel countryViewModel)
        {
            try
            {
                var country = _mapper.Map<Country>(countryViewModel);

                //foreach (var course in countryViewModel.Courses)
                //{
                //    var year = country.EducationTypes.Single(t => t.Name == course.TypeName)
                //        .EducationYears.Single(y => y.Name == course.YearName);
                    
                //    var subject = country.Subjects.Single(s => s.Name == course.SubjectName);
                    
                //    var tempCource = new Course
                //    {
                //        EducationYear = year,
                //        Subject = subject
                //    };
                //    subject.Courses.Add(tempCource);
                //}

                var result = Add(country);
                
                if(result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var id = result.Id;
                country = null; result = null;

                return ResultHelper.Succeeded(data: id);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<int>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<bool> DeleteCountry(int id)
        {
            try
            {
                Delete(id);

                if (!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(data: true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }

        public Result<bool> EditCountry(CountryViewModel countryViewModel)
        {
            try
            {
                //var country = _mapper.Map<Country>(countryViewModel);

                //var educationTypes = _educationTypeManager.Value.GetAll(t => t.CountryId == country.Id, new string[] { "EducationYears" });
                
                //if(!educationTypes.IsSucceeded || educationTypes.Data == null)
                //{
                //    throw new Exception(educationTypes.Message);
                //}

                //var types = educationTypes.Data;
                //types.ForEach(t =>
                //{
                //    t.EducationYears.ToList().ForEach(y =>
                //    {
                //        y.Name = ""
                //    });
                //});

                //SaveChanges();

                return ResultHelper.Succeeded(data: true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<List<CountryViewModel>> GetActiveCountries()
        {
            try
            {
                var countries = GetAll(c => true, "EducationTypes.ExcludedContents", "EducationYears", "Subjects.Language", "Subjects.Courses.Lessons");

                if (countries == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<CountryViewModel>>(countries);

                foreach (var country in result)
                {
                    foreach (var type in country.EducationTypes)
                    {
                        type.EducationYears = country.EducationYears.ToList();
                        foreach (var year in type.EducationYears)
                        {
                            year.Subjects = country.Subjects
                                .Where(s => s.Courses.Any(c => c.EducationYearId == year.Id
                                && !type.ExcludedContents.Any(t => t.CourseId == c.Id))).ToList();
                            foreach (var subject in year.Subjects)
                            {
                                foreach (var course in subject.Courses)
                                {
                                    course.Lessons = course.Lessons.Where(l => !type.ExcludedContents.Any(t => t.LessonId == l.Id)).ToList();
                                }
                            }
                        }
                    }
                }

                return ResultHelper.Succeeded(result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<CountryViewModel>>(message: e.Message);
            }
        }

        public Result<List<CountryViewModel>> GetCountries()
        {
            try
            {
                var countries = GetAll(c => true, new string[] { "EducationTypes.EducationYears", "Subjects.Courses.EducationYear.EducationType" });

                if(countries == null)
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                var result = _mapper.Map<List<CountryViewModel>>(countries);

                return ResultHelper.Succeeded(data: result);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<List<CountryViewModel>>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
