using social_app_client.Repository.Post;
using social_app_client.Repository.User;

namespace social_app_client
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddMvc();
            services.AddHttpContextAccessor();
        }
    }
}
