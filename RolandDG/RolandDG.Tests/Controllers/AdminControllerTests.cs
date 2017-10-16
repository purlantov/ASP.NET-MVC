using AutoMapper;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.Areas.Admin.Controllers;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Account;
using RolandDG.Web.ViewModels.Manage;
using RolandDG.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace RolandDG.Tests.Controllers
{
    [TestFixture]
    public class AdminControllerTests
    {
        // Prtial Arrange
        private readonly Mock<IHttpContextProvider> mockedHttpContext = new Mock<IHttpContextProvider>();
        private readonly Mock<IVerificationProvider> mockedProvider = new Mock<IVerificationProvider>();
        private readonly Mock<IMapper> mockedMapper = new Mock<IMapper>();
        private readonly Mock<IUsersService> mockedUsersService = new Mock<IUsersService>();
        private readonly Mock<IPrintersService> mockedPrinterService = new Mock<IPrintersService>();
        private readonly Mock<IImpactPrintersService> mockedImpactPrinterService = new Mock<IImpactPrintersService>();
        private readonly Mock<IEngraversService> mockedEngraverService = new Mock<IEngraversService>();
        private readonly Mock<IVinylCuttersService> mockedVinylCutterService = new Mock<IVinylCuttersService>();

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenHttpContextIsNull()
        {

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(null, mockedProvider.Object, mockedUsersService.Object,
                    mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, null, mockedUsersService.Object,
                    mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Act & Assert 

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, null,
                    mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            // Act & Assert 
            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                    null, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenPrinterServiceIsNull()
        {
            // Act & Assert 

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                    mockedMapper.Object, null, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenImpactPrinterServiceIsNull()
        {
            // Act & Assert 

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                    mockedMapper.Object, mockedPrinterService.Object, null, mockedEngraverService.Object, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenEngraverServiceIsNull()
        {
            // Act & Assert 

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                    mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, null, mockedVinylCutterService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenVinylCutterServiceIsNull()
        {
            // Act & Assert 

            Assert.Throws<ArgumentNullException>(() =>
                new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                    mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, null));
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminController();

            // Act and Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderView("Index");
        }

        [Test]
        public void AddUserGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddUser())
                .ShouldRenderPartialView("AddUser");
        }

        [Test]
        public void AddUserPOST_ShouldRegister_NewUser()
        {
            //Arrange
            var mockedUser = new Mock<User>();
            var model = new User()
            {
                Email = "pesho@abv.bg"
            };

            var viewModel = new RegisterViewModel()
            {
                Email = "pesho@abv.bg"
            };

            mockedProvider.Setup(v => v.Register(model, "pesho")).Returns(IdentityResult.Success);

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            controller.AddUser(viewModel);

            // Act and Assert
            controller
                .WithCallTo(c => c.AddUser(viewModel))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void Users_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());


            // Act and Assert
            controller
                .WithCallTo(c => c.Users())
                .ShouldRenderPartialView("Users");
        }

        [Test]
        public void UpdateUserGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var user = new User
            {
                Id = "123"
            };

            var usersCollection = new List<User>() { user };

            mockedProvider.Setup(x => x.CurrentUserId).Returns(user.Id);
            mockedUsersService.Setup(c => c.GetAll()).Returns(usersCollection.AsQueryable());


            // Act and Assert
            controller
                .WithCallTo(c => c.UpdateUser(user.Id))
                .ShouldRenderView("UpdateUser");
        }

        [Test]
        public void UpdateUserPOST_ShouldReturnsTrue_IfUser_IsInRoleUser()
        {
            // Arrange
            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var user = new User
            {
                Id = "123"
            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                Role = "Admin"
            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);

            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.AddToRole(user.Id, "Admin"));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "User"));

            //Assert
            controller
                .WithCallTo(c => c.UpdateUser(viewModel))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void UpdateUserPOST_ShouldReturnsTrue_IfUser_IsInRoleAdmin()
        {
            // Arrange
            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var user = new User
            {
                Id = "123"
            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                Role = "User"
            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);

            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.AddToRole(user.Id, "User"));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "Admin"));

            //Assert
            controller
                .WithCallTo(c => c.UpdateUser(viewModel))
                .ShouldRedirectTo(c => c.Index());

        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenComputers_AreValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();

                cfg.CreateMap<ImpactPrinter, ImpactPrinterViewModel>();
                cfg.CreateMap<ImpactPrinterViewModel, ImpactPrinter>();

                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();

                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var printer = new Printer();
            var impactPrinter = new ImpactPrinter();
            var engraver = new Engraver();
            var cutter = new VinylCutter();
            var user = new User();

            var printersCollection = new List<Printer>() { printer };
            var impactPrintersCollection = new List<ImpactPrinter>() { impactPrinter };
            var engraversCollection = new List<Engraver>() { engraver };
            var cuttersCollection = new List<VinylCutter>() { cutter };
            var userssCollection = new List<User>() { user };


            mockedPrinterService.Setup(c => c.GetAll()).Returns(printersCollection.AsQueryable());

            mockedImpactPrinterService.Setup(c => c.GetAll()).Returns(impactPrintersCollection.AsQueryable());

            mockedEngraverService.Setup(c => c.GetAll()).Returns(engraversCollection.AsQueryable());

            mockedVinylCutterService.Setup(c => c.GetAll()).Returns(cuttersCollection.AsQueryable());


            mockedUsersService.Setup(c => c.GetAll()).Returns
                (userssCollection.AsQueryable());

            //Assert
            controller
                .WithCallTo(c => c.Index("Printer", printer.Id)
                .ShouldReturnJson());
        }
    }
}
