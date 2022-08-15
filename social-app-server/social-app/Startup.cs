﻿using social_app.Database;
using social_app.Repositories;

namespace social_app
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }

        public void Configure(IApplicationBuilder app, SocialAppDbContext dbContext) {}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc();
            services.AddHttpContextAccessor();
        }

    }
}
