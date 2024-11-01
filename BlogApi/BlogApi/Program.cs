using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogApi.Data;
using BlogApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlogApiContext") ?? throw new InvalidOperationException("Connection string 'BlogApiContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBlogEndpoints();

app.MapPostEndpoints();

app.Run();
