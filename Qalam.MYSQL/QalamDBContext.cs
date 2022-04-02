using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Seed;

namespace Qalam.MYSQL.Context
{
    public class QalamDBContext : DbContext
    {
        public QalamDBContext(DbContextOptions<QalamDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageItem> PackageItems { get; set; }
        public DbSet<PackageOffer> PackageOffers { get; set; }
        public DbSet<PackagePrice> PackagePrices { get; set; }
        public DbSet<StudentPackage> StudentPackages { get; set; }
        public DbSet<LiveStream> LiveStreams { get; set; }
        public DbSet<StudentVideo> StudentVideos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EducationType> EducationTypes { get; set; }
        public DbSet<EducationYear> EducationYears { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ExcludedContent> ExcludedContents { get; set; }
        public DbSet<DataFile> DataFiles { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<TeacherTimetable> TeacherTimetables { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TeacherFollow> TeacherFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(p => p.NormalizedEmail).IsUnique();

                entity.HasOne(p => p.Country)
                      .WithMany(p => p.Users)
                      .HasForeignKey(p => p.CountryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Role)
                      .WithMany(p => p.Users)
                      .HasForeignKey(p => p.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(p => p.NormalizedName).IsUnique();
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(t => t.UserId)
                      .IsUnique();

                entity.HasOne(p => p.User)
                      .WithOne(p => p.Teacher)
                      .HasForeignKey<Teacher>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Subject)
                      .WithMany(p => p.Teachers)
                      .HasForeignKey(p => p.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasIndex(t => new { t.CourseId, t.TeacherId })
                      .IsUnique();

                entity.HasOne(p => p.Teacher)
                      .WithMany(p => p.Courses)
                      .HasForeignKey(p => p.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Course)
                      .WithMany(p => p.Teachers)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);
            }); 
            
            modelBuilder.Entity<TeacherTimetable>(entity =>
            {
                entity.HasOne(p => p.Teacher)
                      .WithMany(p => p.Timetables)
                      .HasForeignKey(p => p.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Timetable)
                      .WithMany(p => p.Teachers)
                      .HasForeignKey(p => p.TimetableId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TeacherFollow>(entity =>
            {
                entity.HasIndex(t => new { t.TeacherId, t.StudentId })
                      .IsUnique();

                entity.HasOne(p => p.Teacher)
                      .WithMany(p => p.Followers)
                      .HasForeignKey(p => p.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Student)
                      .WithMany(p => p.FollowingTeachers)
                      .HasForeignKey(p => p.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(s => s.UserId)
                      .IsUnique();

                entity.Property(p => p.Points).HasDefaultValue(0);

                entity.HasOne(p => p.User)
                      .WithOne(p => p.Student)
                      .HasForeignKey<Student>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.ReferalByStudent)
                      .WithMany(p => p.InverseReferredBy)
                      .HasForeignKey(p => p.ReferalById)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.EducationYear)
                      .WithMany(p => p.Students)
                      .HasForeignKey(p => p.EducationYearId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentPackage>(entity =>
            {
                entity.HasIndex(s => new { s.StudentId, s.PackageItemId });

                entity.HasOne(p => p.PackageItem)
                      .WithMany(p => p.Students)
                      .HasForeignKey(p => p.PackageItemId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Student)
                      .WithMany(p => p.Packages)
                      .HasForeignKey(p => p.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentExam>(entity =>
            {
                entity.HasIndex(s => new { s.StudentId, s.ExamId })
                      .IsUnique();

                entity.HasOne(p => p.Exam)
                      .WithMany(p => p.Students)
                      .HasForeignKey(p => p.ExamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Student)
                      .WithMany(p => p.Exams)
                      .HasForeignKey(p => p.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasOne(p => p.Setter)
                      .WithMany(p => p.Exams)
                      .HasForeignKey(p => p.SetterId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Video)
                      .WithOne(p => p.Exam)
                      .HasForeignKey<Exam>(p => p.VideoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Course)
                      .WithMany(p => p.Exams)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasIndex(e => new { e.ExamId, e.QuestionId })
                      .IsUnique();

                entity.HasOne(p => p.Exam)
                      .WithMany(p => p.Questions)
                      .HasForeignKey(p => p.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Question)
                      .WithMany(p => p.Exams)
                      .HasForeignKey(p => p.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne(p => p.Lesson)
                      .WithMany(p => p.Questions)
                      .HasForeignKey(p => p.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasIndex(q => q.QuestionId);

                entity.HasOne(p => p.Question)
                      .WithMany(p => p.Answers)
                      .HasForeignKey(p => p.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasOne(p => p.Image)
                      .WithMany(p => p.Packages)
                      .HasForeignKey(p => p.ImageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PackageItem>(entity =>
            {
                entity.HasIndex(p => p.Id).IsUnique();

                entity.HasIndex(p => p.PackageId);

                entity.HasOne(p => p.Package)
                      .WithMany(p => p.Items)
                      .HasForeignKey(p => p.PackageId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<PackageOffer>(entity =>
            {
                entity.HasIndex(p => p.PackageItemId);

                entity.HasOne(p => p.PackageItem)
                      .WithMany(p => p.Offers)
                      .HasForeignKey(p => p.PackageItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PackagePrice>(entity =>
            {
                entity.HasIndex(p => p.PackageItemId);

                entity.HasIndex(p => p.CountryId);

                entity.HasOne(p => p.Country)
                      .WithMany(p => p.PackagePrices)
                      .HasForeignKey(p => p.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.PackageItem)
                      .WithMany(p => p.Prices)
                      .HasForeignKey(p => p.PackageItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LiveStream>(entity =>
            {
                entity.HasIndex(l => new { l.TeacherId, l.CourseId });

                entity.HasOne(p => p.Course)
                      .WithMany(p => p.Videos)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Teacher)
                      .WithMany(p => p.Videos)
                      .HasForeignKey(p => p.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Timetable)
                      .WithMany(p => p.Videos)
                      .HasForeignKey(p => p.TimetableId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<StudentVideo>(entity =>
            {
                entity.HasIndex(s => new { s.StudentId, s.LiveStreamId })
                      .IsUnique();

                entity.HasOne(p => p.LiveStream)
                      .WithMany(p => p.Students)
                      .HasForeignKey(p => p.LiveStreamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Student)
                      .WithMany(p => p.Videos)
                      .HasForeignKey(p => p.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EducationType>(entity =>
            {
                entity.HasOne(p => p.Country)
                      .WithMany(p => p.EducationTypes)
                      .HasForeignKey(p => p.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EducationYear>(entity =>
            {
                entity.HasOne(p => p.Country)
                      .WithMany(p => p.EducationYears)
                      .HasForeignKey(p => p.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(p => p.Country)
                      .WithMany(p => p.Subjects)
                      .HasForeignKey(p => p.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(c => new { c.EducationYearId, c.SubjectId })
                      .IsUnique();

                entity.HasOne(p => p.EducationYear)
                      .WithMany(p => p.Courses)
                      .HasForeignKey(p => p.EducationYearId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Subject)
                      .WithMany(p => p.Courses)
                      .HasForeignKey(p => p.SubjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(p => p.Course)
                      .WithMany(p => p.Lessons)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ExcludedContent>(entity =>
            {
                entity.HasOne(p => p.Course)
                      .WithMany(p => p.ExcludedContents)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Lesson)
                      .WithMany(p => p.ExcludedContents)
                      .HasForeignKey(p => p.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.EducationType)
                      .WithMany(p => p.ExcludedContents)
                      .HasForeignKey(p => p.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.HasOne(p => p.Course)
                      .WithMany(p => p.Timetable)
                      .HasForeignKey(p => p.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DataFile>(entity =>
            {
                entity.HasOne(p => p.User)
                      .WithMany(p => p.Files)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(p => p.Receiver)
                      .WithMany(p => p.ReceivedNotifications)
                      .HasForeignKey(p => p.ReceiverId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Sender)
                      .WithMany(p => p.SentNotifications)
                      .HasForeignKey(p => p.SenderId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Seed();
        }
    }
}