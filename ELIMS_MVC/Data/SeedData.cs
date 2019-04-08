using ELIMS_MVC.Authorization;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELIMS_MVC.Data
{
    public static class SeedData
    {
        #region snipper_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ELIMS_MVCContext(
                serviceProvider.GetRequiredService<DbContextOptions<ELIMS_MVCContext>>()))
            {
                // For sample purposes we are seeding 2 users both with the same password.
                // The password is set with the following command:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                // Create admin user
                var adminid = await EnsureUser(serviceProvider, testUserPw, "admin@elims.com");
                await EnsureRole(serviceProvider, adminid, Constants.ELIMSAdministratorsRole);

                // Created manager
                var managerid = await EnsureUser(serviceProvider, testUserPw, "manager@elims.com");
                await EnsureRole(serviceProvider, managerid, Constants.ELIMSManagersRole);

                // Created regular user
                var userid = await EnsureUser(serviceProvider, testUserPw, "user@elims.com");
                await EnsureRole(serviceProvider, userid, Constants.ELIMSUsersRole);

                SeedDB(context, adminid, managerid, userid);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        #endregion
        //#region snippet1
        public static void SeedDB(ELIMS_MVCContext context, string adminid, string managerid, string userid)
        {
            if (context.Request.Any())
            {
                return;   // DB has been seeded
            }

            // Some sample requests to seed the database with
            //context.Request.AddRange(
            //#region snipped_Request
            //    new Request
            //    {
            //        FirstName = "Bruce",
            //        LastName = "Wayne",
            //        UserId = "1234567",
            //        NAUEmail = "bruce@wayneenterprises.com",
            //        ProjectName = "OMAC",
            //        ProjectObjective = "Observational Metahuman Activity Construct",
            //        ContactName = "Jack Kirby",
            //        ContactID = "135790",
            //        Funding = "N/A",
            //        SponsorName = "Brother Eye",
            //        SponsorPhone = "123-456-7890",
            //        SponsorEmail = "brothereye@wayneenterprises.com",
            //        Chemicals = "N/A",
            //        MeetingTimes = "N/A",
            //        GroupMembers = "Buddy Blank, Alexander Luthor Jr., Max Lord",
            //        ProjectFile = "",
            //        Status = RequestStatus.Pending,
            //        OwnerID = adminid
            //    },
            //#endregion
            //#endregion

             //   new Request
             //   {
             //       FirstName = "Diana",
             //       LastName = "Prince",
             //       UserId = "2468102",
             //       NAUEmail = "wonderwoman@jleague.com",
             //       ProjectName = "Themyscira",
             //       ProjectObjective = "Babysit these baby metahumans",
             //       ContactName = "Patty Jenkins",
             //       ContactID = "135210",
             //       Funding = "DC Comics",
             //       SponsorName = "William Moulton Martson",
             //       SponsorPhone = "123-456-7890",
             //       SponsorEmail = "wmm@dc.com",
             //       Chemicals = "N/A",
             //       MeetingTimes = "N/A",
             //       GroupMembers = "Donna Troy, Cassie Sandsmark, Elizabeth Moulton, Olive Bryne",
             //       ProjectFile = "",
             //       Status = RequestStatus.Approved,
             //       OwnerID = managerid
             //   },
             //   new Request
             //   {
             //       FirstName = "Clark",
             //       LastName = "Kent",
             //       UserId = "9765432",
             //       NAUEmail = "ckent@dailyplanet.com",
             //       ProjectName = "Krypton",
             //       ProjectObjective = "Avoid parental responsibility",
             //       ContactName = "Connor Kent",
             //       ContactID = "135790",
             //       Funding = "N/A",
             //       SponsorName = "Lex Luthor",
             //       SponsorPhone = "123-456-7890",
             //       SponsorEmail = "thelight@cadmus.gov",
             //       Chemicals = "N/A",
             //       MeetingTimes = "N/A",
             //       GroupMembers = "Buddy Blank, Alexander Luthor Jr., Max Lord",
             //       ProjectFile = "",
             //       Status = RequestStatus.Approved,
             //       OwnerID = userid
             //   },
             //   new Request
             //   {
             //       FirstName = "Barry",
             //       LastName = "Allen",
             //       UserId = "7654321",
             //       NAUEmail = "ballen@starlabs.org",
             //       ProjectName = "Gideon",
             //       ProjectObjective = "Artificial intelligence",
             //       ContactName = "Iris West",
             //       ContactID = "0000000",
             //       Funding = "Questionable",
             //       SponsorName = "Harrison Wells",
             //       SponsorPhone = "000-000-0000",
             //       SponsorEmail = "hwells@starlabs.org",
             //       Chemicals = "Who knows",
             //       MeetingTimes = "Tuesdays at 8pm",
             //       GroupMembers = "Cisco Ramon, Caitlin Snow, Joe West, Iris West",
             //       ProjectFile = "",
             //       Status = RequestStatus.Denied,
             //       OwnerID = userid
                //}
             //);
            context.SaveChanges();
        }

    }
}