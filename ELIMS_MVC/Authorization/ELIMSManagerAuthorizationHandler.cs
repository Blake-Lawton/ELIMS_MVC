using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ELIMS_MVC.Authorization
{
    public class ELIMSManagerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Request>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Request resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or deny, they can create entries, and they can update/delete certain ones that are specified in the controllers
            if (requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.DenyOperationName && 
                requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.DeleteOperationName &&
                requirement.Name != Constants.UpdateOperationName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.ELIMSManagersRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
