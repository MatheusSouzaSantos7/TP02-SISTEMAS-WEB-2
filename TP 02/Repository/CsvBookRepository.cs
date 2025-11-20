// Matheus Angelo de Souza Santos
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using TP_02.Models;

namespace TP_02.Repository
{
    public class CsvBookRepository
    {
        private readonly string _path;
        public CsvBookRepository(string path)
        {
            _path = path;
        }

        public void Save(Book book)
        {
            var lines = new List<string>();
            lines.Add($"{book.Name};{book.Price.ToString(CultureInfo.InvariantCulture)};{book.Qty}");
            foreach (var a in book.Authors)
            {
                lines.Add($"{a.Name};{a.Email};{a.Gender}");
            }
            File.WriteAllLines(_path, lines);
        }

        public Book? Load()
        {
            if (!File.Exists(_path)) return null;
            var lines = File.ReadAllLines(_path);
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