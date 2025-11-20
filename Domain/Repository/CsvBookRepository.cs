using Domain.Models;
using System.Globalization;

namespace Domain.Repository
{
    public class CsvBookRepository : IBookRepository
    {
        private readonly string _path;
        public CsvBookRepository(string path)
        {
            _path = path;
        }

        public void Save(Book book)
        {
            var lines = new List<string>();
            // First line: basic book
            lines.Add($"{book.Name};{book.Price.ToString(CultureInfo.InvariantCulture)};{book.Qty}");
            // Authors lines
            foreach (var a in book.Authors)
            {
                lines.Add($"{a.Name};{a.Email};{a.Gender}");
            }
            System.IO.File.WriteAllLines(_path, lines);
        }

        public Book? Load()
        {
            if (!System.IO.File.Exists(_path)) return null;
            var lines = System.IO.File.ReadAllLines(_path);
            if (lines.Length == 0) return null;
            var first = lines[0].Split(';');
            var name = first[0];
            var price = double.Parse(first[1], CultureInfo.InvariantCulture);
            var qty = int.Parse(first[2]);
            var authors = new List<Author>();
            for (int i = 1; i < lines.Length; i++)
            {
                var p = lines[i].Split(';');
                authors.Add(new Author(p[0], p[1], p[2][0]));
            }
            return new Book(name, authors, price, qty);
        }
    }
}