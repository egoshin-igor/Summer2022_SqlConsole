using BlogsApp.Models;
using BlogsApp.Repositories;

const string connectionString = @"Data Source=TLCORE300\SQLEXPRESS;Initial Catalog=BlogsApp4;Pooling=true;Integrated Security=SSPI;TrustServerCertificate=True";

IAuthorRepository authorRepository = new RawSqlAuthorRepository( connectionString );

PrintCommands();
while ( true )
{
    Console.WriteLine( "Введите команду:" );
    string command = Console.ReadLine();

    if ( command == "get-authors" )
    {
        IReadOnlyList<Author> authors = authorRepository.GetAll();
        if ( authors.Count == 0 )
        {
            Console.WriteLine( "Авторы не найдены!" );
            continue;
        }

        foreach ( Author author in authors )
        {
            Console.WriteLine( $"Id: {author.Id}, Name: {author.Name}" );
        }
    }
    else if ( command == "get-by-name" )
    {
        Console.WriteLine( "Введите имя:" );
        string name = Console.ReadLine();
        Author author = authorRepository.GetByName( name );
        if ( author == null )
        {
            Console.WriteLine( "Автор не найден" );
        }
        else
        {
            Console.WriteLine( $"Id: {author.Id}, Name:{author.Name}" );
        }

    }
    else if ( command == "delete-author-by-name" )
    {
        Console.WriteLine( "Введите имя:" );
        string name = Console.ReadLine();
        Author author = authorRepository.GetByName( name );
        if ( author == null )
        {
            Console.WriteLine( "Автор не найден" );
        }
        else
        {
            authorRepository.Delete( author );
            Console.WriteLine( "Автор удален" );
        }
    }
    else if ( command == "update-author" )
    {
        Console.WriteLine( "Введите Id:" );
        int id = int.Parse( Console.ReadLine() );
        Author author = authorRepository.GetById( id );
        if ( author == null )
        {
            Console.WriteLine( "Автор не найден" );
            continue;
        }
        string newName = Console.ReadLine();
        author.UpdateName( newName );

        authorRepository.Update( author );
        Console.WriteLine( "Автор обновлен" );
    }
    else if ( command == "help" )
    {
        PrintCommands();
    }
    else if ( command == "exit" )
    {
        break;
    }
    else
    {
        Console.WriteLine( "Неправильно введенная команда" );
    }
}

void PrintCommands()
{
    Console.WriteLine( "Доступные команды:" );
    Console.WriteLine( "get-authors - Получить список всех авторов" );
    Console.WriteLine( "get-by-name - Получить автора по имени" );
    Console.WriteLine( "delete-author-by-name - Удалить автора по имени" );
    Console.WriteLine( "update-author - Обновить автора" );
    Console.WriteLine( "help - Список команд" );
    Console.WriteLine( "exit - Выход" );
}