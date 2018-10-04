using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportFixtures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Filters
{
    public class AuthorizedRoles : Attribute, IActionFilter
    {
        private Role _role;

        public AuthorizedRoles(Role role)
        {
            _role = role;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string token = context.HttpContext.Request.Headers["Role"];

            if (token.ToLower() != "admin")
            {
                context.Result = new ContentResult()
                {
                    Content = "Role is not admin.",
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
