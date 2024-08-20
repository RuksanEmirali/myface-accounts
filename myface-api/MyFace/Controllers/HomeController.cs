﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyFace.Models.Response;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("")]
    //[BasicAuthorization]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<Dictionary<string, string>> Endpoints()
        {
            return new Dictionary<string, string>
            {
                {"/users", "for information on users."},
                {"/posts", "for information on posts."},
                {"/interactions", "for information about the interactions between users and posts"},
                {"/feeds", "all the data required to build a 'feed' of posts."},
            };
        }
    }
}
