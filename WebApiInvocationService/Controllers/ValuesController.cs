using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiInvocationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://webapivaluesservicesvc/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Values");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string[]>();
                    readTask.Wait();

                    return readTask.Result;

                }
            }
            
            return new string[] { "value a", "value b" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
