using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TP_02.Repository;
using TP_02.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext com MySQL
builder.Services.AddDbContext<LogisticaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("LogisticaDB"),
        new MySqlServerVersion(new Version(8, 0, 33))));

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

// Fixed path to a file inside the web project so it can be edited directly
var csvPath = Path.Combine(builder.Environment.ContentRootPath, "data", "book.csv");
Directory.CreateDirectory(Path.GetDirectoryName(csvPath)!);

var repo = new CsvBookRepository(csvPath);
// If file does not exist, create using sample book
if (!File.Exists(csvPath))
{
    var sampleAuthors = new List<Author>
    {
        new Author("Author One", "one@example.com", 'M'),
        new Author("Author Two", "two@example.com", 'F')
    };
    var sampleBook = new Book("Sample Book", sampleAuthors, 19.9, 3);
    repo.Save(sampleBook);
}

Book? bookFromCsv = repo.Load();

// Minimal endpoints for /livro routes using loaded book
app.MapGet("/livro/Name", () => bookFromCsv?.GetName() ?? "(no book)");
app.MapGet("/livro/ToString", () => bookFromCsv?.ToString() ?? "(no book)");
app.MapGet("/livro/GetAuthorNames", () => bookFromCsv?.GetAuthorNames() ?? "(no book)");
app.MapGet("/livro/ApresentarLivro", () =>
{
    if (bookFromCsv == null) return Results.Content("<html><body><h1>No book</h1></body></html>", "text/html");
    var authorsHtml = string.Join("", bookFromCsv.Authors.Select(a => $"<li>{a.Name}</li>"));
    var html = $"<html><body><h1>{bookFromCsv.Name}</h1><ul>{authorsHtml}</ul></body></html>";
    return Results.Content(html, "text/html");
});

// Test endpoint: demonstrates all Book methods and shows results as JSON
app.MapGet("/livro/Test", () =>
{
    var b = bookFromCsv ?? new Book("Sample Test Book", new List<Author> {
        new Author("T1","t1@example.com",'M'), new Author("T2","t2@example.com",'F') }, 9.99, 1);

    var originalPrice = b.GetPrice();
    var originalQty = b.GetQty();
    var name = b.GetName();
    var authors = b.GetAuthors();
    var toString = b.ToString();
    var authorNames = b.GetAuthorNames();

    // mutate using setters
    b.SetPrice(originalPrice + 5);
    b.SetQty(originalQty + 2);

    var afterPrice = b.GetPrice();
    var afterQty = b.GetQty();

    return Results.Json(new
    {
        Name = name,
        ToString = toString,
        AuthorNames = authorNames,
        Authors = authors.Select(a => new { a.Name, a.Email, a.Gender }),
        OriginalPrice = originalPrice,
        AfterPrice = afterPrice,
        OriginalQty = originalQty,
        AfterQty = afterQty
    });
});

// Endpoint to reload CSV at runtime
app.MapPost("/livro/reload", () =>
{
    bookFromCsv = repo.Load();
    return bookFromCsv is null ? Results.NotFound("no file") : Results.Ok("reloaded");
});

// Endpoint to save posted Book JSON to CSV
app.MapPost("/livro/save", (Book updated) =>
{
    repo.Save(updated);
    bookFromCsv = updated;
    return Results.Ok("saved");
});

app.Run();
