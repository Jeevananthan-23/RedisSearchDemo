using System.Collections.Generic;

namespace RedisSearchDemo.Models
{
    public class Cart
    {
        public string Id { get; set; } 
        public string UserId { get; set; }

        public HashSet<CartItem> CartItems { get; set; }
 /*
        public int count()
        {
            return getCartItems().size();
        }
        public double getTotal()
        {
            return cartItems //
                .stream() 
                .mapToDouble(ci->ci.getPrice() * ci.getQuantity()) //
                .sum();
        }*/
    }
}
