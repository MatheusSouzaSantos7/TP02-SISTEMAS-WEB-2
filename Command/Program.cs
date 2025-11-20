using Domain.Models;
using Domain.Repository;

Console.WriteLine("Teste Book/Author - Console");

var authors = new List<Author>
{
    new Author("Author One", "one@example.com", 'M'),
    new Author("Author Two", "two@example.com", 'F')
};

var book = new Book("Sample Book", authors, 29.9, 5);

Console.WriteLine("Name: " + book.GetName());
Console.WriteLine("ToString: " + book.ToString());
Console.WriteLine("Authors: " + book.GetAuthorNames());
Console.WriteLine("Price: " + book.GetPrice());
Console.WriteLine("Qty: " + book.GetQty());

var repoPath = Path.Combine(Environment.CurrentDirectory, "book.csv");
var repo = new CsvBookRepository(repoPath);
repo.Save(book);

Console.WriteLine($"Saved book to {repoPath}");

var loaded = repo.Load();
Console.WriteLine("Loaded: " + (loaded == null ? "(null)" : loaded.ToString()));

Console.WriteLine("Press ENTER to exit...");
Console.ReadLine();