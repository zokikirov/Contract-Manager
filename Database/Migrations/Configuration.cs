namespace DatabaseNamespace.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DatabaseNamespace;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity;
    using DatabaseNamespace.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseNamespace.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseNamespace.DatabaseContext context)
        {

			User user = null;

			//password is Qwerty12#3
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<User>(new UserStore<User>(context));
			if (!context.Roles.Any())
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				user = new User
				{
					UserName = "admin@cegeka.com",
					PhoneNumber = "08869879",
					Email = "admin@cegeka.com",
					Name = "Admin",
					SecurityStamp = DateTime.Now.ToString(),
					CompanyName = "Cegeka"
				};

				string userPWD = "Qwerty12#3";

				var chkUser = UserManager.Create(user, userPWD);

				//Add default User to Role Admin   
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");
				}

				role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Manager";
				roleManager.Create(role);

				role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "User";
				roleManager.Create(role);				
			}
            if (!context.MailConfigurations.Any())
            {
                MailConfiguration mailConfig = new MailConfiguration();
                mailConfig.Username = "gdprcontract";
                mailConfig.Password = "qwerty12#3";
                mailConfig.Host = "smtp.gmail.com";
                mailConfig.Port = 587;
                mailConfig.MailType = MailServerType.SMTP;
                mailConfig.From = "gdprcontract@gmail.com";
                mailConfig.EnableSsl = true;
                context.MailConfigurations.Add(mailConfig);
            }

            context.SaveChanges();
        }
    }
}
