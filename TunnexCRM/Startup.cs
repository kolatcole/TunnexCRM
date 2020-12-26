using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
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
                                      builder.AllowAnyOrigin().//.WithOrigins("https://tunnexcrm.netlify.app", "http://localhost:4200")/*.WithOrigins("https://tunnexlabcrm.com")*//*.WithOrigins("https://tunnexcrm.netlify.app", "http://localhost:4200")*/.
                                                                            AllowAnyHeader()
                                                                            .AllowAnyMethod().AllowAnyOrigin();


                                    // FOR PRODUCTION  builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://tunnexlabcrm.com");
                                      //  AllowAnyHeader()
                                      //.AllowAnyMethod().AllowAnyOrigin();
                                  });
            });
            //builder.WithOrigins("https://tunnexcrm.netlify.app", "http://localhost:4200")/*.WithOrigins("https://tunnexlabcrm.com")*//*.WithOrigins("https://tunnexcrm.netlify.app", "http://localhost:4200")*/.
            //                                        AllowAnyHeader()
            //                                      .AllowAnyMethod().AllowAnyOrigin();
            //services.AddCors();
            //add cors support
            
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
            services.AddScoped<IRepo<SkillorKPI>, SkillorKPIRepo>();
            services.AddScoped<ISkillorKPIRepo, SkillorKPIRepo>();
            services.AddScoped<IRepo<Staff>, StaffRepo>();
            services.AddScoped<IRepo<Qualification>, QualificationRepo>();
            services.AddScoped<IRepo<Assessment>, AssessmentRepo>();
            services.AddScoped<IRepo<StaffSkillorKPI>, StaffSkillorKPIRepo>();
            services.AddScoped<IStaffSkillorKPIRepo, StaffSkillorKPIRepo>();
            services.AddScoped<IRepo<CustomerMessage>, CustomerMessageRepo>();
            services.AddScoped<IQuotationRepo, QuotationRepo>();
            services.AddScoped<IRepo<QuotProduct>, QuotProductRepo>();
            services.AddScoped<IWaybillRepo, WaybillRepo>();
            services.AddScoped<IRepo<WaybillProduct>, WaybillProductRepo>();
            services.AddScoped<IRepo<Supplier>, SupplierRepo>();
            services.AddScoped<IRepo<PurchaseProduct>, PurchaseProductRepo>();
            services.AddScoped<IPurchaseRepo, PurchaseRepo>();
            services.AddScoped<IReturnedStockRepo, ReturnedStockRepo >();


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
            services.AddTransient<IQuotationService, QuotationService>();
            services.AddTransient<IWaybillService, WaybillService>();
            services.AddTransient<IPurchaseService, PurchaseService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
               


            }
            
            // Make sure you call this before calling app.UseMvc()
            //app.UseCors(
            //    options => options.WithOrigins("https://tunnexlabcrm.com", "https://www.tunnexlabcrm.com").AllowAnyMethod().AllowAnyHeader()
            //);

            app.UseRouting();
            
            // must be after routing and before user authorization

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
