using MongoDB.Driver;

public class BooksContext
{
    private readonly IMongoDatabase _database;

    public BooksContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
}
