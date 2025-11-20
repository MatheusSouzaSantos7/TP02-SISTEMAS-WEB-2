using Domain.Models;

namespace Domain.Repository
{
    public interface IBookRepository
    {
        void Save(Book book);
        Book? Load();
    }
}