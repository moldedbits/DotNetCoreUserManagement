﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserAppService.InputModel;
using UserAppService.Service;
using UserAppService.OutputModels;

namespace UserAppService.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        #region CTORs

        private readonly IAuthService _service;

        public LoginController(IAuthService service)
        {
            _service = service;
        }

        #endregion

        //[Authorize]
        [HttpGet]
        [Route("api/GetCurrentLogin")]
        public string Get()
        {
            return "User";
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/ExternalLogin/Google")]
        public GoogleExternalLoginResult PostExternalGoogleLogin([FromBody] GoogleLoginInputModel inputModel)
        {
            //return Ok(_service.GoogleLogin(inputModel));
            //return (IHttpActionResult)Ok(_service.GoogleLogin(inputModel));
            return _service.GoogleLogin(inputModel);
        }
    }


}