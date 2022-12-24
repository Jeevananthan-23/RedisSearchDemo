using Redis.OM.Contracts;
using Redis.OM.Searching;
using Redis.OM;
using RedisSearchDemo.Models;

namespace RedisSearchDemo.Helper
{
  public class CreateBookIndex
  {
    private readonly IRedisConnection _connection;
    public CreateBookIndex(RedisConnectionProvider connectionProvider)
    {
    _connection = connectionProvider.Connection;
    }

    public async Task CreateIndex()
    {
    var index = await _connection.GetIndexInfoAsync(typeof(Book));
    if (index == null)
    {
      await _connection.CreateIndexAsync(typeof(Book));
    }
    }

    public async Task<List<string>> GetAuthorsAutoComplete(string query)
    {
    var ret = new List<string>();
    var args = new[] { "sugg:book:author", query };
    RedisReply[] res = await _connection.ExecuteAsync("FT.SUGGET", args);
    for (var i = 0; i < res.Length; i++)
    {
      ret.Add(res[i]);
    }

    return ret;
    }

    public async Task<long> AddSuggestions(string key, string suggestionString, float score, object? payload = null)
    {
    var args = new List<string>() { key, suggestionString, score.ToString(), payload.ToString()};
    return await _connection.ExecuteAsync("FT.SUGADD", args.ToArray());
    }
  }
}
