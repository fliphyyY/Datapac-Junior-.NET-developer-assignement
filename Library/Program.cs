using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Library.BookContext;
using Library.Context;
using Library.Database;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Database") ??
                       throw new InvalidOperationException();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<LibraryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IBookInfo, BookInfo>();
builder.Services.AddScoped<IBookCollectionGateway, BookCollectionGateway>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

var app = builder.Build();







app.UseRouting();
app.UseStatusCodePages();
app.UseCors(myAllowSpecificOrigins);
app.UseStaticFiles();
app.MapControllers();
app.Run();



app.Run();
