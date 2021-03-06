﻿using ELIMS_MVC.Data;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELIMS_MVC.Authorization
{
    public class UserIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Request>
    {
        UserManager<ApplicationUser> _userManager;

        public UserIsOwnerAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Request resource)
        {

            if (context.User == null || resource == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }

            // If we're not asking for CRUD permission, return.
            if (requirement.Name != Constants.CreateOperationName && requirement.Name != Constants.ReadOperationName && requirement.Name != Constants.UpdateOperationName && requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }


            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
