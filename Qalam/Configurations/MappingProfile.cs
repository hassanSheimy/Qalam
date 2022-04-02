using AutoMapper;
using Qalam.BLL.ViewModels;
using Qalam.MYSQL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Qalam.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Qalam.Common.Enums;

namespace Qalam.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegisterViewModel>();
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.NormalizedEmail, src => src.MapFrom(x => x.Email.ToUpper()));

            CreateMap<User, UserViewModel>()
               .ForMember(dest => dest.ImagePath, src => src.MapFrom(x => x.Files.FirstOrDefault(f => f.Type == EFileTypes.ProfileImage).FilePath))
               .ForMember(dest => dest.EducationYearId, src => src.MapFrom(x => x.Student != null ? x.Student.EducationYearId : 0))
               .ForMember(dest => dest.SubjectId, src => src.MapFrom(x => x.Teacher != null ? x.Teacher.Subject.Id : 0))
               .ForMember(dest => dest.UniqueKey, src => src.MapFrom(x => x.Teacher != null ? x.Teacher.StreamKey : x.Student != null ? x.Student.Referalcode : null));

            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();

            CreateMap<TeacherRegisterViewModel, Teacher>();
            CreateMap<StudentRegisterViewModel, Student>();

            CreateMap<Student, StudentPointViewModel>();

            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.User.FullName))
                .ForMember(dest => dest.About, src => src.MapFrom(x => x.User.About))
                .ForMember(dest => dest.Facebook, src => src.MapFrom(x => x.User.Facebook))
                .ForMember(dest => dest.ImagePath, src => src.MapFrom(x => x.User.Files.FirstOrDefault(f => f.Type == EFileTypes.ProfileImage).FilePath))
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Subject.Name))
                .ForMember(dest => dest.EducationYears, src => src.MapFrom(x => x.Courses.Select(y => y.Course.EducationYear.Name)))
                .ForMember(dest => dest.CountryName, src => src.MapFrom(x => x.User.Country.Name));
            
            CreateMap<Teacher, TeacherInfoViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.User.FullName))
                .ForMember(dest => dest.IsConfirmed, src => src.MapFrom(x => x.User.IsConfirmed))
                .ForMember(dest => dest.ImagePath, src => src.MapFrom(x => x.User.Files.FirstOrDefault(f => f.Type == EFileTypes.ProfileImage).FilePath))
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Subject.Name));

            CreateMap<TeacherCourse, TeacherCourseViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Course.EducationYear.Name));
            CreateMap<TeacherCourseViewModel, TeacherCourse>();

            CreateMap<Package, PackageViewModel>()
                .ForMember(dest => dest.ImagePath, src => src.MapFrom(x => x.Image.FilePath));
            CreateMap<PackageViewModel, Package>()
                .ForMember(dest => dest.Image, src => src.Ignore());

            CreateMap<PackageItem, PackageItemViewModel>()
                .ForMember(dest => dest.PriceInMoney, src => src.MapFrom((x, d, v, r) => x.Prices.FirstOrDefault(p => p.CountryId == (int)r.Items["countryId"]).PriceInMoney))
                .ForMember(dest => dest.PriceInPoint, src => src.MapFrom((x, d, v, r) => x.Prices.FirstOrDefault(p => p.CountryId == (int)r.Items["countryId"]).PriceInPoint))
                .ForMember(dest => dest.Offer, src => src.MapFrom(x => x.Offers.FirstOrDefault(o => o.ExpiryDate >= DateTime.UtcNow)));
            CreateMap<PackageItemViewModel, PackageItem>();

            CreateMap<PackageOffer, PackageOfferViewModel>();
            CreateMap<PackageOfferViewModel, PackageOffer>();

            CreateMap<LiveStream, LessonVideoMinimalViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Course.Subject.Name))
                .ForMember(dest => dest.TeacherName, src => src.MapFrom(x => x.Teacher.User.FullName))
                .ForMember(dest => dest.EducationYear, src => src.MapFrom(x => x.Course.EducationYear.Name));
            CreateMap<LessonVideoMinimalViewModel, LiveStream>();

            CreateMap<LiveStream, LessonVideoInfoViewModel>()
                .ForMember(dest => dest.EducationYearName, src => src.MapFrom(x => x.Course.EducationYear.Name))
                .ForMember(dest => dest.Views, src => src.MapFrom(x => x.Students.Count))
                .ForMember(dest => dest.Rating, src => src.MapFrom(x => x.Students.Count > 0 ? x.Students.Average(s => s.Rate) : 0));
            CreateMap<LessonVideoInfoViewModel, LiveStream>();

            CreateMap<LiveStream, LiveStreamViewModel>()
                .ForMember(dest => dest.CountryName, src => src.MapFrom(x => x.Course.EducationYear.Country.Name))
                .ForMember(dest => dest.EducationYearName, src => src.MapFrom(x => x.Course.EducationYear.Name))
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Course.Subject.Name))
                .ForMember(dest => dest.TeacherName, src => src.MapFrom(x => x.Teacher.User.FullName))
                .ForMember(dest => dest.MyProperty, src => src.MapFrom((x, d, v, r) => x.Students.FirstOrDefault(s => s.StudentId == (int?)r.Items["studentId"]).Id));
            CreateMap<LiveStreamViewModel, LiveStream>();

            CreateMap<StudentPackage, StudentPackageViewModel>()
                .ForMember(dest => dest.Color, src => src.MapFrom(x => x.PackageItem.Package.Color))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.PackageItem.Name))
                .ForMember(dest => dest.Image, src => src.MapFrom(x => x.PackageItem.Package.Image.FilePath));
            
            CreateMap<User, StudentPointViewModel>()
                .ForMember(dest => dest.Points, src => src.MapFrom(x => x.Student.Points));

            CreateMap<StudentExam, StudentExamsDegreeModel>()
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Exam.Course.Subject.Name))
                .ForMember(dest => dest.TeacherName, src => src.MapFrom(x => (x.Exam.Setter.User.FullName)));

            CreateMap<Exam, StudentExamsDegreeModel>()
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Course.Subject.Name))
                .ForMember(dest => dest.TeacherName, src => src.MapFrom(x => (x.Setter.User.FullName)));

            CreateMap<Exam, ExamViewModel>()
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Course.Subject.Name))
                .ForMember(dest => dest.SubjectId, src => src.MapFrom(x => x.Course.Subject.Id));
            CreateMap<ExamViewModel, Exam>();

            CreateMap<StudentVideoViewModel, StudentVideo>();

            CreateMap<QuestionViewModel, Question>(); 
            CreateMap<Question, QuestionViewModel>();

            CreateMap<Question, QuestionExamViewModel>();
            CreateMap<QuestionExamViewModel, Question>();

            CreateMap<QuestionAnswer, QuestionAnswerExamViewModel>();
            CreateMap<QuestionAnswerExamViewModel, QuestionAnswer>();

            CreateMap<QuestionAnswerViewModel, QuestionAnswer>();
            CreateMap<QuestionAnswer, QuestionAnswerViewModel>();

            CreateMap<FollowTeacherViewModel, TeacherFollow>();

            CreateMap<Timetable, TimetableViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Course.Subject.Name))
                .ForMember(dest => dest.Country, src => src.MapFrom(x => x.Course.Subject.Country))
                .ForMember(dest => dest.EducationYear, src => src.MapFrom(x => x.Course.EducationYear))
                .ForMember(dest => dest.Start, src => src.MapFrom(x => DateTime.Today.StartOfWeek(DayOfWeek.Saturday).AddDays((int)x.Day.DayOfWeek(DayOfWeek.Saturday)).ToString("yyyy-MM-dd")
                                                                       + " " + x.Start.ToString(@"hh\:mm")))
                .ForMember(dest => dest.End, src => src.MapFrom(x => DateTime.Today.StartOfWeek(DayOfWeek.Saturday).AddDays((int)x.Day.DayOfWeek(DayOfWeek.Saturday)).ToString("yyyy-MM-dd")
                                                                       + " " + x.End.ToString(@"hh\:mm")))
                .ForMember(dest => dest.StartTime, src => src.MapFrom(x => x.Start.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(x => x.End.ToString(@"hh\:mm")))
                .ForMember(dest => dest.IsInterval, src => src.MapFrom(x => true))
                .ForMember(dest => dest.Day, src => src.MapFrom(x => ((int)x.Day + 1) % 7));

            CreateMap<TimetableViewModel, Timetable>()
                .ForMember(dest => dest.Start, src => src.MapFrom(x => TimeSpan.Parse(x.StartTime)))
                .ForMember(dest => dest.End, src => src.MapFrom(x => TimeSpan.Parse(x.EndTime)));

            CreateMap<TeacherTimetable, TimetableViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Timetable.Course.Subject.Name))
                .ForMember(dest => dest.Country, src => src.MapFrom(x => x.Timetable.Course.Subject.Country))
                .ForMember(dest => dest.EducationYear, src => src.MapFrom(x => x.Timetable.Course.EducationYear))
                .ForMember(dest => dest.Start, src => src.MapFrom(x => DateTime.Today.StartOfWeek(DayOfWeek.Saturday).AddDays((int)x.Timetable.Day.DayOfWeek(DayOfWeek.Saturday)).ToString("yyyy-MM-dd")
                                                                       + " " + x.Start.ToString(@"hh\:mm")))
                .ForMember(dest => dest.End, src => src.MapFrom(x => DateTime.Today.StartOfWeek(DayOfWeek.Saturday).AddDays((int)x.Timetable.Day.DayOfWeek(DayOfWeek.Saturday)).ToString("yyyy-MM-dd")
                                                                       + " " + x.Start.Add(new TimeSpan(1, 0, 0)).ToString(@"hh\:mm")))
                .ForMember(dest => dest.StartTime, src => src.MapFrom(x => x.Start.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(x => x.Start.Add(new TimeSpan(1, 0, 0)).ToString(@"hh\:mm")))
                .ForMember(dest => dest.IsInterval, src => src.MapFrom(x => false))
                .ForMember(dest => dest.Day, src => src.MapFrom(x => ((int)x.Timetable.Day + 1) % 7));

            CreateMap<TeacherTimetable, LiveStreamViewModel>()
                .ForMember(dest => dest.CourseId, src => src.MapFrom(x => x.Timetable.CourseId))
                .ForMember(dest => dest.TimetableId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.Ignore());

            CreateMap<TeacherTimetable, TimetableMinimalViewModel>()
                .ForMember(dest => dest.EducationYearName, src => src.MapFrom(x => x.Timetable.Course.EducationYear.Name))
                .ForMember(dest => dest.SubjectName, src => src.MapFrom(x => x.Timetable.Course.Subject.Name))
                .ForMember(dest => dest.TeacherName, src => src.MapFrom(x => x.Teacher.User.FullName))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => DateTime.Today.StartOfWeek(DayOfWeek.Saturday).AddDays((int)x.Timetable.Day.DayOfWeek(DayOfWeek.Saturday)).ToString("yyyy-MM-dd")
                                                                       + " " + x.Start.Add(new TimeSpan(1, 0, 0)).ToString(@"hh\:mm")));

            CreateMap<NotificationViewModel, Notification>()
                .ForMember(dest => dest.Sender, src => src.Ignore());
            CreateMap<Notification, NotificationViewModel>();

            CreateMap<User, NotificationSenderViewModel>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(x => x.FullName))
                .ForMember(dest => dest.ImagePath, src => src.MapFrom(x => x.Files.FirstOrDefault(f => f.Type == EFileTypes.ProfileImage).FilePath));

            #region Lookups
            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>();

            CreateMap<EducationTypeViewModel, EducationType>();
            CreateMap<EducationType, EducationTypeViewModel>()
                .ForMember(dest => dest.EducationYears, src => src.MapFrom((x, d, v, r) => (r.Items["obj"] as Country).EducationYears));

            CreateMap<EducationYearViewModel, EducationYear>();
            CreateMap<EducationYear, EducationYearViewModel>()
                .ForMember(dest => dest.Subjects, src => src.MapFrom((x, d, v, r) => (r.Items["obj"] as Country).Subjects));

            CreateMap<SubjectViewModel, Subject>();
            CreateMap<Subject, SubjectViewModel>();

            CreateMap<CourseViewModel, Course>();
            CreateMap<Course, CourseViewModel>();

            CreateMap<LessonViewModel, Lesson>();
            CreateMap<Lesson, LessonViewModel>();

            CreateMap<Language, LanguageViewModel>();
            CreateMap<LanguageViewModel, Language>();
            #endregion

        }
    }
}
