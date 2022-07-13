using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using BlogsApp.Models;

namespace BlogsApp.Repositories
{
    public class RawSqlAuthorRepository : IAuthorRepository
    {
        private readonly string _connectionString;

        public RawSqlAuthorRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Author> GetAll()
        {
            var result = new List<Author>();

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name] from [Author]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while ( reader.Read() )
            {
                result.Add( new Author(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] )
                ) );
            }

            return result;
        }

        public Author GetByName( string name )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name] from [Author] where [Name] = @name";
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 100 ).Value = name;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Author(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ) );
            }
            else
            {
                return null;
            }
        }

        public Author GetById( int id )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name] from [Author] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Author(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ) );
            }
            else
            {
                return null;
            }
        }

        public void Delete( Author author )
        {
            if ( author == null )
            {
                throw new ArgumentNullException( nameof( author ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Author] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = author.Id;
            sqlCommand.ExecuteNonQuery();
        }

        public void Update( Author author )
        {
            if ( author == null )
            {
                throw new ArgumentNullException( nameof( author ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Author] set [Name] = @name where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = author.Id;
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 100 ).Value = author.Name;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
