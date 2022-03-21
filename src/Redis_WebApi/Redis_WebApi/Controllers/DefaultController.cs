using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Redis_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public DefaultController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("/one")]
        public async Task<IActionResult> One([FromServices] ServiceOne serviceOne, [FromQuery] int n = 1)
        {
            var res = await _cache.GetOrAddAsync(n, serviceOne.GetCarAsync);
            return Ok(res);
        }
        [HttpGet("/two")]
        public async Task<IActionResult> two([FromServices] ServiceTwo serviceTwo, [FromQuery] string key = "foo")
        {
            var res = await _cache.GetOrAddAsync(key,  serviceTwo.GetNameAsync);

            return Ok(res);
        }
        [HttpGet("/three")]
        public async Task<IActionResult> Three([FromServices] ServiceThree serviceThree, [FromQuery] int n = 1)
        {
            return Ok();
        }
    }
}
