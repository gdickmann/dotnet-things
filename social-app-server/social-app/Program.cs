using social_app.Database;
using Microsoft.EntityFrameworkCore;
using social_app.gRPC.Services;
using social_app.RabbitMQ.Services;

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

var app = builder.Build();
app.MapGrpcService<UserService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await new HostBuilder().ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<PostService>();
    }).RunConsoleAsync();

app.Run();

/** RabbitMQ configuration */