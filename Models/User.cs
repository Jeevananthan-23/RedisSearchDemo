using System.Collections.Generic;

namespace RedisSearchDemo.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public HashSet<Role> roles = new HashSet<Role>();
        public void addRole(Role role)
        {
            roles.Add(role);
        }

        private ISet<Book> books = new HashSet<Book>();

        public void addBook(Book book)
        {
            books.Add(book);
        }
    }
}
