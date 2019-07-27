using Api.Customer.Repository;
using Api.Customer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Api.Customer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{Configuration["Version"]}", new Info
                {
                    Title = Configuration["Name"],
                    Version = Configuration["Version"]
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            ConfigSwagger(app);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            var connectionString = Configuration["Database:ConnectionString"];
            services.AddScoped<IDbConnectionFactory>(cfgProvider => new CustomerSQLiteDatabase(connectionString));
        }

        private void ConfigSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint($"v{Configuration["Version"]}/swagger.json",
                    $"{Configuration["Name"]} {Configuration["Version"]}");
            });
        }
    }
}