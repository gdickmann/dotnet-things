using social_app_client.RabbitMQ;

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
            services.AddScoped<IMessageProducer, MessageProducer>();

            services.AddMvc();
            services.AddHttpContextAccessor();
        }
    }
}
