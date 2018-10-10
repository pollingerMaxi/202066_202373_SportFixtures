using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data;
using SportFixtures.Data.Entities;
using System;

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
        }

        private static IUserBusinessLogic GetLogic(ActionExecutingContext context)
        {
            return (IUserBusinessLogic)context.HttpContext.RequestServices.GetService(typeof(IUserBusinessLogic));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];

            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    Content = "Token authorization is required to use this service.",
                };
            }

            var logic = GetLogic(context);
            var user = logic.TokenIsValid(token);

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
    }
}
