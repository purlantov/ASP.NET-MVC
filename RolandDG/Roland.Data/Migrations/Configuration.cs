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
                for (int i = 1; i < 6; i++)
                {
                    var printer = new Printer()
                    {
                        Model = "SP-" + i + "40",
                        ProductType = "Printer",
                        PrintHeads = i,
                        MediaWidth = 600 + (9 + i),
                        Ink = InkType.EcoSolvent,
                        MaxSpeed = 10
                    };

                    context.Printers.Add(printer);
                }
            }

            if (!context.Engravers.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    var engraver = new Engraver()
                    {
                        Model = "EGX-" + (i+1) + "00",
                        ProductType = "Engraver",
                        RPM = 8000+(i*1000),
                        TableWidth = i+320,
                        TableDepth = 500,
                        MaxSpeed = 10
                    };

                    context.Engravers.Add(engraver);
                }
            }

            if (!context.VinylCutters.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    var cutter = new VinylCutter()
                    {
                        Model = "GX-" + (i + 1) + "00",
                        ProductType = "Vinyl Cutter",
                        CuttingSpeed = 30,
                        BladeForce = 30,
                        MediaWidth = 300+(200*i)
                    };

                    context.VinylCutters.Add(cutter);
                }
            }

            if (!context.ImpactPrinters.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    var impactPrinter = new ImpactPrinter()
                    {
                        Model = "MPX-9" + i,
                        ProductType = "Impact Printer",
                        Resolution = 800
                    };

                    context.ImpactPrinters.Add(impactPrinter);
                }
            }
        }
    }
}
