using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using RedisSearchDemo.Helper;
using RedisSearchDemo.Models;
using System.Reflection;

namespace RedisSearchDemo.Repositories
{
  public class BookRepository
  {
    private readonly IRedisConnection _connection;
    private readonly IRedisCollection<Book> _bookCollection;
    private readonly CreateBookIndex _createBookIndex;
    public BookRepository(RedisConnectionProvider connectionProvider)
    {
    _connection = connectionProvider.Connection;
    _bookCollection = connectionProvider.RedisCollection<Book>(chunkSize:1000);
    _createBookIndex = new CreateBookIndex(connectionProvider);
    }

    public async Task<bool> AddBook(Book book)
    {
    var result = await _bookCollection.InsertAsync(book);
    await _createBookIndex.AddSuggestions("sugg:book:title", book.Title, 1, book);
    return result != null;
    }

    // add book map to getallbook
    public async ValueTask<IList<Book>> GetAllBooks()
    {
    return await _bookCollection.ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBookByTitle(string title)
    {
     return await _bookCollection.Where(x => x.Title.Contains(title)).ToListAsync();
    }

    public async Task<List<Suggestion>> GetTitleAutoComplete(string perfix, bool WITHPAYLOAD = true)
    {
    var args = new List<string>();
    var ret = new List<Suggestion>();
    args.Add("sugg:book:title");
    args.Add(perfix);
    if (WITHPAYLOAD)
    {
      args.Add("WITHPAYLOADS");
    }
    RedisReply[] res = await _connection.ExecuteAsync("FT.SUGGET", args.ToArray());
    for (var i = 0; i < res.Length; i += 2)
    {
      ret.Add(new Suggestion { Name = res[i], Payload = res[i + 1] });
    }

    return ret;
    }

    public async Task<IList<Book>> PaginateBooks(string query, int page, string sortBy = "Title", string direction = "ASC", int pageSize = 10)
    {

    var results = await _bookCollection.Where(x =>
                x.Title.Contains(query) ||
                x.Subtitle.Contains(query) ||
                x.Authors.Contains(query) ||
                x.Description.Contains(query) ||
                x.Language.Contains(query)).
                Skip(page * pageSize).
                Take(pageSize).
                ToListAsync();

    if (direction == "ASC")
    {
      return results.OrderBy(x => x.GetType().GetProperty(sortBy)?.GetValue(x, null)).ToList();
    }
    else if(direction == "DECS")
    {
      return results.OrderByDescending(x => x.GetType().GetProperty(sortBy)?.GetValue(x, null)).ToList();
    }

    return results;
    }
  }
}
