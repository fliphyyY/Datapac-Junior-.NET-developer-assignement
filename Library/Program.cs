using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Library.BookContext;
using Library.Context;
using Library.Database;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Database") ??
                       throw new InvalidOperationException();


builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<LibraryDbContext>()
    .AddDefaultTokenProviders();



var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    using (var context = new LibraryDbContext(optionsBuilder.Options))
    {
        if (!context.Database.CanConnect())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }


builder.Services.AddScoped<IBookInfo, BookInfo>();
builder.Services.AddScoped<IBookCollectionGateway, BookCollectionGateway>();

builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();
app.Run();
