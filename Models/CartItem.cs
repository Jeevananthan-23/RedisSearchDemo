using System.Runtime.InteropServices;

namespace RedisSearchDemo.Models
{
  public class CartItem
  {
    public string Isbn { get; set; }
    public double Price { get; set; }
    public long Quantity { get; set; }
  }
}
