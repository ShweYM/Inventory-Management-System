using Inventory_Management_System.DB;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inventory_Management_System
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {

                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = _env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.Name = "InventoryManagement.AuthCookie";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/access-denied";

                    //Configuration.Bind("CookieSettings", options)
                });

            services.AddSession();

            services.AddTransient<DashboardService>();
            services.AddTransient<DelegationService>();
            services.AddTransient<EmployeeService>();
            services.AddTransient<EmployeeTypeService>();
            services.AddTransient<DepartmentService>();
            services.AddTransient<InventoryTransactionService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ProductCategoryService>();
            services.AddTransient<RequisitionService>();
            services.AddTransient<DisbursementFormService>();
            services.AddTransient<DisbursementFormProductService>();
            services.AddTransient<CollectionPointService>();
            services.AddTransient<StationeryRetrievalFormService>();
            services.AddTransient<StationeryRetrievalRequisitionFormService>();
            services.AddTransient<DisbursementFormRequisitionFormProductService>();
            services.AddTransient<SupplierService>();
            services.AddTransient<SupplierProductService>();
            services.AddTransient<PurchaseOrderService>();
            services.AddTransient<DeliveryOrderService>();
            services.AddTransient<PurchaseDeliveryProductService>();
            services.AddTransient<DeliveryOrderSupplierProductService>();
            services.AddTransient<PurchaseOrderSupplierProductService>();
            services.AddTransient<DisbursementFormRequisitionFormService>();
            services.AddTransient<TrendAnalysisService>();
            services.AddDbContext<InventoryManagementSystemContext>(opt =>
            opt.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DbConn")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            InventoryManagementSystemContext db,
            IWebHostEnvironment environment,
            ProductService pService,
            DepartmentService dService,
            InventoryTransactionService invtransService,
            ProductCategoryService pcService,
            DisbursementFormService dfService,
            StationeryRetrievalFormService srfService,
            EmployeeTypeService etService,
            EmployeeService empService,
            RequisitionService rfService,
            DelegationService delService,
            SupplierService supService,
            SupplierProductService spService,
            InventoryTransactionService itService)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Cookie authentication configuration
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            new DBSeeder(db, environment, pService, dService, pcService, dfService, srfService,
                etService, empService, rfService, delService, supService, spService, itService);

        }
    }
}
