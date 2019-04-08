using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;


namespace ELIMS_MVC.Authorization
{
    public static class ELIMSOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };
        public static OperationAuthorizationRequirement Deny =
          new OperationAuthorizationRequirement { Name = Constants.DenyOperationName };
    }

    public class Constants
    {
        // Operations
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string DenyOperationName = "Deny";

        // User roles
        // Administrators = Lab admins and lab management
        // Managers = Between users and admins, like student managers; users with permissions
        // Users = standard users
        public static readonly string ELIMSAdministratorsRole = "Administrators";
        public static readonly string ELIMSManagersRole = "Managers";
        public static readonly string ELIMSUsersRole = "Users";
    }
}
