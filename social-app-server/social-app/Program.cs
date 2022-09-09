using social_app.Database;
using Microsoft.EntityFrameworkCore;
using social_app.gRPC.Services;
using social_app.RabbitMQ.Services;
using social_app.Repositories.Post;
using social_app.Repositories.User;

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

builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddHostedService<PostService>();

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