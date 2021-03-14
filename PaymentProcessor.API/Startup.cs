using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentProcessor.API.PaymentGateways;
using PaymentProcessor.API.Repositories;
using PaymentProcessor.API.Services;
using PaymentProcessor.Data;

namespace PaymentProcessor.API
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
            //Database
            services.AddDbContext<PaymentProcessorDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PaymentProcessorDbContext")
                ,x=> x.MigrationsAssembly("PaymentProcessor.Data"))
                ,ServiceLifetime.Transient
            );

            //Automapper
            var mapperConfig = new MapperConfiguration(mc =>
              mc.AddProfile(new ModelMapperProfile())
              );
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Dependency
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPaymentProcessor, Services.PaymentProcessor>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
