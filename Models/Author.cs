namespace BlogsApp.Models
{
    public class Author
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Author( int id, string name )
        {
            Id = id;
            Name = name;
        }

        public void UpdateName( string newName )
        {
            Name = newName;
        }
    }
}
