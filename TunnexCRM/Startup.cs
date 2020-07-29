using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Domains;
using CRMSystem.Infrastructure;
using CRMSystem.Presentation.Core.Setup_Files;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;


namespace CRMSystem
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://tunnexlabcrm.com")/*.WithOrigins("https://tunnexcrm.netlify.app", "http://localhost:4200")*/.
                                                    AllowAnyHeader()
                                                  .AllowAnyMethod()/*.AllowAnyOrigin()*/;
                                  });
            });

           //services.AddCors();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version="v1",
                    Title="CRM System API"
                });
            });
            services.AddDbContext<TContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultTLocal"), b => b.MigrationsAssembly("CRMSystem.Presentation.Core"));
            });

            services.AddScoped<IRepo<Lead>, LeadRepo>();
            services.AddScoped<IRepo<Message>, MessageRepo>();
            services.AddScoped<IRepo<User>, UserRepo>();
            services.AddScoped<IRepo<Product>, ProductRepo>();
            services.AddScoped<IRepo<Price>, PriceRepo>();
            services.AddScoped<IRepo<Customer>, CustomerRepo>();
            services.AddScoped<IRepo<Sale>, SaleRepo>();
            services.AddScoped<IRepo<Item>, ItemRepo>();
            services.AddScoped<IRepo<Role>, RoleRepo>();
            services.AddScoped<IRepo<Privilege>, PrivilegeRepo>();
            services.AddScoped<IRepo<Cart>, CartRepo>();
            services.AddScoped<IRepo<Invoice>, InvoiceRepo>();
            services.AddScoped<IRepo<Payment>, PaymentRepo>();
            services.AddScoped<IPaymentRepo, PaymentRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<ISaleRepo, SaleRepo>();
            services.AddScoped<IRepo<Skill>, SkillRepo>();
            services.AddScoped<IRepo<Staff>, StaffRepo>();
            services.AddScoped<IRepo<Qualification>, QualificationRepo>();
            services.AddScoped<IRepo<Assessment>, AssessmentRepo>();
            services.AddScoped<IRepo<StaffSkill>, StaffSkillRepo>();
            services.AddScoped<IStaffSkillRepo, StaffSkillRepo>();
            services.AddScoped<IRepo<CustomerMessage>, CustomerMessageRepo>();



            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISaleService, SaleService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<ILeadService, LeadService>();
            services.AddTransient<IStaffSkillService, StaffSkillService>();
            services.AddTransient<ICustomerService, CustomerService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
               


            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddSerilog(); // <-- enable Serilog Middleware
            var swaggerOpt = new SwaggerOpt();
            Configuration.GetSection(nameof(SwaggerOpt)).Bind(swaggerOpt);
            app.UseSwagger(opt => {
                opt.RouteTemplate = swaggerOpt.JsonRoute;
            });
            app.UseSwaggerUI(opt=> {
                opt.SwaggerEndpoint(swaggerOpt.UIEndPoint, swaggerOpt.Description);
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
