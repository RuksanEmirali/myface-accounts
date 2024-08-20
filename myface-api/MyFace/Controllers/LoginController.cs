using System;
using Microsoft.AspNetCore.Mvc;
using MyFace.Models.Response;

namespace MyFace.Controllers
{
    public class LoginController
    {
        [ApiController]
        [Route("/login")]
        //[BasicAuthorization]
        public class OAuthController : Controller
        {
            [HttpPost("token"),BasicAuthorization]
            public IActionResult Index()
            {
                Console.WriteLine("We are in Login!!!");
                return Ok();
            }
        }
    }
}