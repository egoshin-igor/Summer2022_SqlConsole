using BlogsApp.Models;

namespace BlogsApp.Repositories
{
    public interface IAuthorRepository
    {
        IReadOnlyList<Author> GetAll();
        Author GetByName( string name );
        Author GetById( int id );
        void Update( Author author );
        void Delete( Author author );
    }
}
