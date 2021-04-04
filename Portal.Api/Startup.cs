using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Portal.Api.Infrastructure;
using Portal.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Portal.Service;
using Portal.Interface;
using Portal.Context;
using His.Reception.Application.Service;
using Portal.Application.Interface;
using Portal.Application.Service;
using Portal.DTO;
using His.Reception.Application.Interface;
using Portal.Application.Interface.Base;
using Portal.Application.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;

namespace Portal.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "The value '{0}' is not valid for {1}");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "A value for the '{0}' property was not provided");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "A value is required.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "A non-empty request body is required.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "The value '{0}' is not valid.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "The supplied value is invalid.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "The field must be a number.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "The supplied value is invalid for {0}.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "The value '{0}' is invalid.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "The field {0} must be a number.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "The field {0} not must null");

                //  options.ModelBindingMessageProvider.s

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddJsonOptions(options =>
            {
                //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


            });

            services.AddControllers();

            #region option 

            services.Configure<ConfigSmsDto>(Configuration.GetSection("configSms"));

            #endregion

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            RegisterService(services);
            RegisterHttpService(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portal APIs By Backend Team", Version = "v1" });

                c.OperationFilter<SwaggerAddRequiredHeaderParameter>();
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                //  c.AddFluentValidationRules();

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });

                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (x) =>
                {
                    c.IncludeXmlComments(x);
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(option => option
                    .UseLoggerFactory(LoggerFactory.Create(x => x.AddDebug()))
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                optionsLifetime: ServiceLifetime.Scoped
            );

            //services.AddDbContext<LibraryContext>(options => options
            //    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            //    .UseSqlServer(Configuration.GetConnectionString("LibraryDemoSql")));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                                    new CultureInfo("fa-IR"),
                                    new CultureInfo("en-US"),
                                    new CultureInfo("ar-IQ")
                };

                options.DefaultRequestCulture = new RequestCulture("ar-IQ");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRequestHandler();
            app.UseSession();

            app.UseCors("MyPolicy");

            #region language Resource

            var supportedCultures = new List<CultureInfo>
                                {
                                    new CultureInfo("fa-IR"),
                                    new CultureInfo("en-US"),
                                    new CultureInfo("ar-IQ"),
                                };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ar-IQ"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            //app.UseRequestLocalization(options);

            //  set lang
            //app.Use( async (context,next) =>
            //{
            //    var lang = context.Request.Headers["Accept-Language"].ToString();
            //    var cultureInfo = CultureInfo.GetCultureInfo(lang);

            //    Thread.CurrentThread.CurrentCulture = cultureInfo;
            //    Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //    await next.Invoke();
            //});

            #endregion

            #region swagger 

            //set language from httpcontext to static CurrentLanguage


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            });

            #endregion

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void RegisterHttpService(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<ISmsService, SmsService>();
        }

        public void RegisterService(IServiceCollection services)
        {
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IWorkContextService, WorkContextService>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IFileGroupService, FileGroupService>();
            services.AddScoped<IFileManagerService, FileManagerService>();
            services.AddScoped<IAnswerServiceService, AnswerServiceService>();
            services.AddScoped<IZoneService, ZoneService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IReceptionsService, ReceptionsService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IPatientCardService, PatientCardService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IPcrReportService, PcrReportService>();
            services.AddScoped<IPortalResourceService, PortalResourceService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped(typeof(IBasicService<>), typeof(BasicService<>));
            services.AddScoped<ISyncService, SyncService>();

        }
    }
}
