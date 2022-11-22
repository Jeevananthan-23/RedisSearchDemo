# RedisSearchDemo
```
    Authors
+--------------+
|  name        |               Author-Books
|              |            +----------------+
|  author_id   +------------+  author_id     |
|              |            |                |
+--------------+        +---+  book_isbn     |
                        |   |                |
    Users               |   +----------------+
+--------------+        |
|  first_name  |        |
|              |        |
|  last_name   |        |       Checkouts
|              |        |  +------------------------+
|  email       |   +----|--+  user_id               |
|              |   |    |  |                        |
|  user_id     +---+    +--+  book_isbn             |
|              |        |  |                        |
|  last_login  |        |  |  checkout_date         |
|              |        |  |                        |
+--------------+        |  |  checkout_length_days  |
                        |  |                        |
    Books               |  |  geopoint              |
+--------------+        |  |                        |
|  isbn        +--------+  +------------------------+
|              |
|  title       |
|              |
|  subtitle    |
|              |
|  thumbnail   |
|              |
|  description |
|              |
|  categories  |
|              |
|  authors     |
|              |
|  author_ids  |
|              |
+--------------+
```
