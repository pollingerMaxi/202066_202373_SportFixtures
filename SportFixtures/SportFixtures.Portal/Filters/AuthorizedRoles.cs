using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
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
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    Content = "Token authorization is required to use this service.",
                };
            }

            User user;
            using (var logic = GetLogic(context))
            {
                user = logic.TokenIsValid(token);
            }

            //var user = userBL.TokenIsValid(token);

            if (user == null)
            {
                context.Result = new ContentResult()
                {
                    Content = "Token is invalid. Please provide a valid token to use this service.",
                };
            }

            if (user.Role != Role.Admin)
            {
                context.Result = new ContentResult()
                {
                    Content = $"User is not in role: {_role}",
                };
            }
        }

        private static IUserBusinessLogic GetLogic(ActionExecutedContext context)
        {
            return (IUserBusinessLogic)context.HttpContext.RequestServices.GetService(typeof(IUserBusinessLogic));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
