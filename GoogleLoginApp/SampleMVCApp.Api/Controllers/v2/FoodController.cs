using System;
using Microsoft.AspNetCore.Mvc;

namespace SampleMVCApp.Api.Controllers.v2
{
    //There isn't anything here, its just to show that there could be
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FoodController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("2.0");
        }
    }
}
