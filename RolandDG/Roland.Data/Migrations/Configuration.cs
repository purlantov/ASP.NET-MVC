namespace Roland.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "info@roland.com";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Roland.Data.MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);

            base.Seed(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleName = "Admin";
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now,
                    UserType = UserType.Admin
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (!context.Printers.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var printer = new Printer()
                    {
                        Model = "MDX-4" + i,
                        ProductType = ProductType.Printer + i,
                        PrintHeads = i + 1,
                        MediaWidth = 600 + (9 * i),
                        Ink = InkType.EcoSolvent,
                        MaxSpeed = 10
                    };

                    context.Printers.Add(printer);
                }
            }
        }
    }
}
