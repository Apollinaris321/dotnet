using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Microsoft.OpenApi.Models;
using Todo.Data;
using Todo.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Blog API",
        Description = "Post your blog",
        Version = "v1" });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddDbContext<DataContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog Api V1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
