using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELIMS_MVC.Data;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELIMS_MVC.Models
{
    public class BasePageModel : PageModel
    {
        protected ELIMS_MVCContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<ApplicationUser> UserManager { get; }

        public BasePageModel(
            ELIMS_MVCContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
