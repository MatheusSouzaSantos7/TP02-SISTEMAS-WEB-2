using System.Text;

namespace Domain.Models
{
    public class Book
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

        public Book(string name, List<Author> authors, double price)
        {
            Name = name;
            Authors = authors;
            Price = price;
            Qty = 0;
        }

        public Book(string name, List<Author> authors, double price, int qty)
        {
            Name = name;
            Authors = authors;
            Price = price;
            Qty = qty;
        }

        public string GetName() => Name;
        public List<Author> GetAuthors() => Authors;
        public double GetPrice() => Price;
        public void SetPrice(double price) => Price = price;
        public int GetQty() => Qty;
        public void SetQty(int qty) => Qty = qty;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Book[name={Name},authors={{{string.Join(',', Authors)} }},price={Price},qty={Qty}]");
            return sb.ToString();
        }

        public string GetAuthorNames() => string.Join(",", Authors.Select(a => a.Name));
    }
}