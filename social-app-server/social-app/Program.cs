using social_app.Database;
using Microsoft.EntityFrameworkCore;
using social_app.gRPC.Services;
using MassTransit;
using social_app.RabbitMQ.Services;
using social_app.RabbitMQ.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// gRPC
builder.Services.AddGrpc();
// Database
builder.Services.AddDbContext<SocialAppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
// MassTransit (RabbitMQ)
await Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<PostService>(typeof(PostServiceDefinition));
        x.SetKebabCaseEndpointNameFormatter();
        x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
    });
}).Build().RunAsync();

RabbitMqHelper.StartRabbit();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<UserService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
