using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using RedisSearchDemo.Models;
using System.Text.Json;

namespace RedisSearchDemo.Helper
{
  public class Seeder
  {
    private static IRedisConnection? _connection;
    private static IRedisCollection<Book> _bookCollection;
    public static async ValueTask Seed(IServiceScope serviceScope)
    {
    var args = new List<string>();
    var books = new List<Book>();
    var connectionProvider = serviceScope.ServiceProvider.GetService<RedisConnectionProvider>();
     
      _connection = connectionProvider.Connection;
      _bookCollection = connectionProvider.RedisCollection<Book>();

      using (var reader = new StreamReader("./Data/books.csv"))
      {
        reader.ReadLine(); // Skip the first line (header row)

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
        var fields = line.Split(",");
        if (fields.Length <= 12)
        {
          var book = new Book()
          {
            Id = Convert.ToInt32(fields[1]),
            Title = fields[2],
            Subtitle = fields[3],
            Authors = fields[4].Split().ToArray(),
            Categories = fields[5],
            Thumbnail = fields[6],
            Description = fields[7],
            Year = Convert.ToInt32(fields[8]),
            PageCount = Convert.ToInt64(fields[10]),
            Rating = new BookRating()
            {
            Id = fields[1],
            Count = Convert.ToInt32(fields[11]),
            Rating = Convert.ToDecimal(fields[9])
            }
          };
          books.Add(book);
        }
        }
      }

      _connection.DropIndexAndAssociatedRecords(typeof(Book));
      _connection.Unlink("sugg:book:title");
      var index = _connection.CreateIndex(typeof(Book));
      if (index)
      {
      var tasks = new List<Task>();
        foreach(var book in books)
        {
        tasks.Add(_connection.SetAsync(book));
        var bk = new Book()
        {
          Title = book.Title,
          Thumbnail = book.Thumbnail,
          Year = book.Year,
          Rating = book.Rating,
        };
        args.Add("sugg:book:title");
        args.Add(bk.Title);
        args.Add("1.0");
        args.Add("PAYLOAD");
        args.Add(JsonSerializer.Serialize(bk, options: new JsonSerializerOptions { IgnoreNullValues = true }));
         await _connection.ExecuteAsync("FT.SUGADD", args.ToArray());
        args.Clear();
        }
        await Task.WhenAll(tasks);
      }
    }

  }
}
