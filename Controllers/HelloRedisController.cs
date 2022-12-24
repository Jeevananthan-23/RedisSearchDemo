using Microsoft.AspNetCore.Mvc;
using Redis.OM;
using Redis.OM.Contracts;

namespace RedisSearchDemo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HelloRedisPingController : ControllerBase
  {
    private readonly IRedisConnection _connection;
    public HelloRedisPingController(RedisConnectionProvider connectionProvider)
    {
    _connection = connectionProvider.Connection;
    }

    [HttpGet]
    public async Task<ActionResult<string>> PingRedis()
    {
    var reply = await _connection.ExecuteAsync("Ping");
    return Ok(reply.ToString());
    }
  }
}
