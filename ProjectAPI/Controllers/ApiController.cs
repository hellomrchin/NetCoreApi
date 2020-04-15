using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Model;

namespace ProjectAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private ApiBL _apiBL;

        public ApiController(ApiBL context)
        {
            this._apiBL = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ApiDM>> GetApi()
        {
            _apiBL = HttpContext.RequestServices.GetService(typeof(ApiBL)) as ApiBL;
            return _apiBL.GetApiList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ApiDM>> GetApi(string id)
        {
            _apiBL = HttpContext.RequestServices.GetService(typeof(ApiBL)) as ApiBL;
            return _apiBL.GetApiList(id);
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public string CreateApi([FromForm] ApiDM apiDm)
        {
            _apiBL = HttpContext.RequestServices.GetService(typeof(ApiBL)) as ApiBL;
            return _apiBL.CreateApi(apiDm);
        }

        [HttpPut("{id}")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public string UpdateApi([FromRoute]string id, [FromForm] ApiDM apiDm)
        {
            _apiBL = HttpContext.RequestServices.GetService(typeof(ApiBL)) as ApiBL;
            return _apiBL.UpdateApi(id, apiDm);
        }

        [HttpDelete("{id}")]
        public string DeleteApi(string id)
        {
            _apiBL = HttpContext.RequestServices.GetService(typeof(ApiBL)) as ApiBL;
            return _apiBL.DeleteApi(id);
        }


    }
}