namespace RedisSearchDemo.Models
{
    public class BookRating
    {
        public string Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
    }
}
