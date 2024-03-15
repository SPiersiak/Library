using Books.Api.Models;
using Books.Api.Repositories;
using Books.Api.Repositories.Interfaces;
using Books.Api.Services;
using Books.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddDbContextFactory<LibraryContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Library")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
