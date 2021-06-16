using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.WebAPI.Controllers
{
    [Authorize]
    //Controller base is used for API only no views are going to be used 
    [Route("api/[Controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Get/api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {

            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete]
        public void Delete(int id)
        {

        }
    }
}
