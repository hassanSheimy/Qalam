using Microsoft.EntityFrameworkCore;
using Qalam.MYSQL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.MYSQL.Seed
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Role
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Teacher",
                    NormalizedName = "TEACHER",
                },
                new Role
                {
                    Id = 2,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                },
                new Role
                {
                    Id = 3,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                }
            );
            #endregion

            #region Languages
            modelBuilder.Entity<Language>().HasData(
                new Language
                {
                    Id = 1,
                    Name = "اللغة العربية",
                    Code = "ar",
                    IsRTL = true
                },
                new Language
                {
                    Id = 2,
                    Name = "English",
                    Code = "en",
                    IsRTL = false
                },
                new Language
                {
                    Id = 3,
                    Name = "French",
                    Code = "fr",
                    IsRTL = false
                },
                new Language
                {
                    Id = 4,
                    Name = "Italian",
                    Code = "it",
                    IsRTL = false
                },
                new Language
                {
                    Id = 5,
                    Name = "German",
                    Code = "de",
                    IsRTL = false
                }
            );
            #endregion

            #region Countries
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "مصر",
                    Currency = "جنيه"
                },
                new Country
                {
                    Id = 2,
                    Name = "عمان",
                    Currency = "ريال"
                }
            );
            #endregion

            #region Education Types
            modelBuilder.Entity<EducationType>().HasData(
                new EducationType
                {
                    Id = 1,
                    Name = "التعليم العام",
                    CountryId = 1
                },
                new EducationType
                {
                    Id = 2,
                    Name = "التعليم الأزهري",
                    CountryId = 1
                }
            );
            #endregion

            #region Education Years
            modelBuilder.Entity<EducationYear>().HasData(
                new EducationYear
                {
                    Id = 1,
                    Name = "الصف الأول الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 2,
                    Name = "الصف الثاني الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 3,
                    Name = "الصف الثالث الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 4,
                    Name = "الصف الرابع الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 5,
                    Name = "الصف الخامس الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 6,
                    Name = "الصف السادس الإبتدائي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 7,
                    Name = "الصف الأول الإعدادي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 8,
                    Name = "الصف الثاني الإعدادي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 9,
                    Name = "الصف الثالث الإعدادي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 10,
                    Name = "الصف الأول الثانوي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 11,
                    Name = "الصف الثاني الثانوي",
                    CountryId = 1
                },
                new EducationYear
                {
                    Id = 12,
                    Name = "الصف الثالث الثانوي",
                    CountryId = 1
                }
            );
            #endregion

            #region Subjects
            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    Id = 1,
                    Name = "اللغة العربية",
                    CountryId = 1,
                    LanguageId = 1
                },
                new Subject
                {
                    Id = 2,
                    Name = "الرياضيات",
                    CountryId = 1,
                    LanguageId = 1
                },
                new Subject
                {
                    Id = 3,
                    Name = "English",
                    CountryId = 1,
                    LanguageId = 2
                },
                new Subject
                {
                    Id = 4,
                    Name = "العلوم",
                    CountryId = 1,
                    LanguageId = 1
                },
                new Subject
                {
                    Id = 5,
                    Name = "الدراسات الإجتماعية",
                    CountryId = 1,
                    LanguageId = 1
                }
            );
            #endregion

            #region Courses
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    SubjectId = 1,
                    EducationYearId = 1
                },
                new Course
                {
                    Id = 2,
                    SubjectId = 2,
                    EducationYearId = 1
                },
                new Course
                {
                    Id = 3,
                    SubjectId = 3,
                    EducationYearId = 1
                },
                new Course
                {
                    Id = 4,
                    SubjectId = 1,
                    EducationYearId = 2
                },
                new Course
                {
                    Id = 5,
                    SubjectId = 2,
                    EducationYearId = 2
                },
                new Course
                {
                    Id = 6,
                    SubjectId = 3,
                    EducationYearId = 2
                },
                new Course
                {
                    Id = 7,
                    SubjectId = 1,
                    EducationYearId = 3
                },
                new Course
                {
                    Id = 8,
                    SubjectId = 2,
                    EducationYearId = 3
                },
                new Course
                {
                    Id = 9,
                    SubjectId = 3,
                    EducationYearId = 3
                },
                new Course
                {
                    Id = 10,
                    SubjectId = 1,
                    EducationYearId = 4
                },
                new Course
                {
                    Id = 11,
                    SubjectId = 2,
                    EducationYearId = 4
                },
                new Course
                {
                    Id = 12,
                    SubjectId = 3,
                    EducationYearId = 4
                }
            );
            #endregion

            #region Subject Units
            modelBuilder.Entity<Lesson>().HasData(
                new Lesson
                {
                    Id = 1,
                    Name = "أسره سعيدة",
                    CourseId = 1,
                    UnitNumber = 1,
                    SemesterNumber = 1
                },
                new Lesson
                {
                    Id = 2,
                    Name = "مدرستي جميلة",
                    CourseId = 1,
                    UnitNumber = 2,
                    SemesterNumber = 1
                },
                new Lesson
                {
                    Id = 3,
                    Name = "نشيد أمي",
                    CourseId = 1,
                    UnitNumber = 3,
                    SemesterNumber = 1
                }
            );
            #endregion

            #region Files
            modelBuilder.Entity<DataFile>().HasData(
                new DataFile
                {
                    Id = 1,
                    FileName = "livePackage_",
                    UploadDate = new DateTime(2020, 8, 15),
                    FilePath = ""
                },
                new DataFile
                {
                    Id = 2,
                    FileName = "examPackage",
                    UploadDate = new DateTime(2020, 8, 15),
                    FilePath = ""
                },
                new DataFile
                {
                    Id = 3,
                    FileName = "allPackage",
                    UploadDate = new DateTime(2020, 8, 15),
                    FilePath = ""
                }
            );
            #endregion

            #region Packages
            modelBuilder.Entity<Package>().HasData(
                new Package
                {
                    Id = 1,
                    Name = "باقة قلم مباشر",
                    Items = null,
                    ImageId = 1,
                    Color = "#789e5f",
                    PeriodInDays = 30
                },
                new Package
                {
                    Id = 2,
                    Name = "باقة الإمتحانات",
                    Items = null,
                    ImageId = 2,
                    Color = "#7030a0",
                    PeriodInDays = 30
                },
                new Package
                {
                    Id = 3,
                    Name = "الباقة الحرة",
                    Items = null,
                    ImageId = 3,
                    Color = "#00b0f0",
                    PeriodInDays = 30
                }
            );
            #endregion

            #region Packages' Items
            modelBuilder.Entity<PackageItem>().HasData(
                new PackageItem
                {
                    Id = 1,
                    Name = "4 حصص مباشر",
                    NumberOfExams = 0,
                    NumberOfVideos = 4,
                    PackageId = 1
                },
                new PackageItem
                {
                    Id = 2,
                    Name = "8 حصص مباشر",
                    NumberOfExams = 0,
                    NumberOfVideos = 8,
                    PackageId = 1
                },
                new PackageItem
                {
                    Id = 3,
                    Name = "28 حصه مباشر",
                    NumberOfExams = 0,
                    NumberOfVideos = 28,
                    PackageId = 1
                },
                new PackageItem
                {
                    Id = 4,
                    Name = "20 امتحان لأي مادة",
                    NumberOfExams = 20,
                    NumberOfVideos = 0,
                    PackageId = 2
                },
                new PackageItem
                {
                    Id = 5,
                    Name = "40 امتحان لأي مادة",
                    NumberOfExams = 40,
                    NumberOfVideos = 0,
                    PackageId = 2
                },
                new PackageItem
                {
                    Id = 6,
                    Name = "65 امتحان لأي مادة",
                    NumberOfExams = 65,
                    NumberOfVideos = 0,
                    PackageId = 2
                },
                new PackageItem
                {
                    Id = 7,
                    Name = "5 حصص مباشر - 30 امتحان",
                    NumberOfExams = 30,
                    NumberOfVideos = 5,
                    PackageId = 3
                },
                new PackageItem
                {
                    Id = 8,
                    Name = "10 حصص مباشر - 60 امتحان",
                    NumberOfExams = 60,
                    NumberOfVideos = 10,
                    PackageId = 3
                }
            );
            #endregion

            #region Packages' Prices
            modelBuilder.Entity<PackagePrice>().HasData(
                new PackagePrice
                {
                    Id = 1,
                    CountryId = 1,
                    PackageItemId = 1,
                    PriceInMoney = 60
                },
                new PackagePrice
                {
                    Id = 2,
                    CountryId = 1,
                    PackageItemId = 2,
                    PriceInMoney = 80
                },
                new PackagePrice
                {
                    Id = 3,
                    CountryId = 1,
                    PackageItemId = 3,
                    PriceInMoney = 100
                },
                new PackagePrice
                {
                    Id = 4,
                    CountryId = 1,
                    PackageItemId = 4,
                    PriceInMoney = 40
                },
                new PackagePrice
                {
                    Id = 5,
                    CountryId = 1,
                    PackageItemId = 5,
                    PriceInMoney = 50
                },
                new PackagePrice
                {
                    Id = 6,
                    CountryId = 1,
                    PackageItemId = 6,
                    PriceInMoney = 60
                },
                new PackagePrice
                {
                    Id = 7,
                    CountryId = 1,
                    PackageItemId = 7,
                    PriceInMoney = 50
                },
                new PackagePrice
                {
                    Id = 8,
                    CountryId = 1,
                    PackageItemId = 8,
                    PriceInMoney = 80
                }
            );
            #endregion
        }
    }
}
