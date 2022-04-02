using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Qalam.BLL.Hubs;
using Qalam.BLL.Managers;
using Qalam.BLL.Services;
using Qalam.Common.Helper;
using Qalam.Configurations;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;

namespace Qalam
{
    public class Startup
    {
        private readonly string ConnectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ConnectionString = configuration["ConnectionStrings:Local"];
            }
            else
            {
                ConnectionString = configuration["ConnectionStrings:UAT"];
            }
        }

        internal class Lazier<T> : Lazy<T> where T : class
        {
            public Lazier(IServiceProvider provider) : base(() => provider.GetRequiredService<T>()) { }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            var mappingConfig = new MapperConfiguration(mapperProfile =>
            {
                mapperProfile.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            #region Dependency Injection
            services.AddMemoryCache();

            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
            services.AddSingleton(mapper);
            services.AddScoped(typeof(RequestAttributes));
            services.AddScoped(typeof(SystemManager));

            services.AddScoped(typeof(CsvManager));
            services.AddScoped(typeof(UserManager));
            services.AddScoped(typeof(TokenManager));
            
            services.AddScoped(typeof(PackageManager));
            services.AddScoped(typeof(PackageItemManager));
            services.AddScoped(typeof(PackageOfferManager));

            services.AddScoped(typeof(LiveStreamManager));
            services.AddScoped(typeof(TeacherTimetableManager));
            services.AddScoped(typeof(TimetableManager));
            services.AddScoped(typeof(TeacherManager));
            services.AddScoped(typeof(TeacherCourseManager));

            services.AddScoped(typeof(UploaderManager));

            services.AddScoped(typeof(CountryManager));
            services.AddScoped(typeof(CourseManager));
            services.AddScoped(typeof(LanguageManager));

            services.AddScoped(typeof(ExamManager));
            services.AddScoped(typeof(QuestionManager));

            services.AddScoped(typeof(StudentPackageManager));
            services.AddScoped(typeof(StudentExamManager));
            services.AddScoped(typeof(StudentManager));
            services.AddScoped(typeof(StudentVideoManager));
            services.AddScoped(typeof(TeacherFollowManager));
            
            services.AddScoped(typeof(NotificationManager));
            services.AddScoped(typeof(LiveHandler));
            services.AddScoped(typeof(NotificationHandler));

            services.AddScoped(typeof(MailService));
            #endregion

            services.AddDbContext<QalamDBContext>(options => options.UseMySql(ConnectionString));

            services.AddMvcCore();

            services.AddSignalR(signalR => signalR.EnableDetailedErrors = true);

            // In production, the VUE files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Name = "Basic",
                    Description = "Please Enter Your Authrization JWT token",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Basic"
                            }
                        }, new List<string>()
                    }
                });

                s.SwaggerDoc("V1", new OpenApiInfo { Version = "V1", Title = "Qalam" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options => options
                .SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<RequestMiddleware>();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
                routes.MapHub<QalamHub>("/hub/qalam-real-time");
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/V1/swagger.json", "Qalam V1");
            });
        }
    }
}
