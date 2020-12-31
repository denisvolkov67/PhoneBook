﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Web.Controllers
{

    [ApiController]
    public class AccountController : Controller
    {
        [Route("account/name")]
        [HttpGet]
        public string GetName()
        {
            IPrincipal p = HttpContext.User;
            return p.Identity.Name;
        }

        [Route("account/role")]
        [HttpGet]
        public string GetRole()
        {
            IPrincipal p = HttpContext.User;
            if (p.IsInRole("Phonebook_Edit"))
                return "true";
            else
                return "false";
        }
    }
}
