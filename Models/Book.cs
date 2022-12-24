using Redis.OM.Modeling;

namespace RedisSearchDemo.Models
{
  [Document(StorageType = StorageType.Json, IndexName = "books-idx" , Stopwords = new[] {"The", "  "," \"", ".", "...","as","In"})]
  public class Book
  {
    [RedisField]
    [Indexed(Sortable =true)]
    public int Id { get; set; }

    [Searchable(Sortable = true,Weight = 10.0)]
    public string Title { get; set; }

    [Indexed]
    public string Subtitle { get; set; }

    [Searchable(Normalize = true,CascadeDepth =4)]
    public string Description { get; set; }

    [Indexed(CascadeDepth = 2)]
    public string Language { get; set; }

    [Indexed(CascadeDepth = 3)]
    public string Categories { get; set; }

    [Indexed(Sortable = true)]
    public long PageCount { get; set; }

    public string Thumbnail { get; set; }

    [Indexed(Sortable = true)]
    public int Year { get; set; }

    [Indexed(CascadeDepth = 5)]
    public string[] Authors { get; set; }

    [Indexed(CascadeDepth = 3)]
    public BookRating Rating { get; set; }
  }
}
