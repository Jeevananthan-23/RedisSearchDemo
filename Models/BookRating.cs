using Redis.OM.Modeling;

namespace RedisSearchDemo.Models
{
  [Document(StorageType = StorageType.Json, IndexName = "bookrating-idx")]
  public class BookRating
  {
    [Indexed]
    public string Id { get; set; }

    public User User { get; set; }

    [Indexed(Sortable = true)]
    public decimal Rating { get; set; }

    [Indexed(Sortable = true)]
    public int Count { get; set; }
  }
}
