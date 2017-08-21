using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemD8.Models;
using Microsoft.AspNetCore.Identity;

namespace InMemD8.Data
{
    public class DbInitializer
    {

        public static void Initialize(UserManager<ApplicationUser> userManager)
        {
            var aUser = new ApplicationUser();
            aUser.UserName = "student@test.com";
            aUser.Email = "student@test.com";
            var r = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;
        }
    }
}
