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
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using RolandDG.Tests.Providers;
using RolandDG.Web.Areas.Admin;
using TestStack.FluentMVCTesting;

namespace RolandDG.Tests.Controllers
{
    //public static class FakeController
    //{
    //    public static void MakeAjaxRequest(this Controller controller)
    //    {
    //        // First create request with X-Requested-With header set
    //        Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
    //        httpRequest.SetupGet(x => x.Headers).Returns(
    //            new WebHeaderCollection() {
    //                {"X-Requested-With", "XMLHttpRequest"}
    //            }
    //        );

    //        // Then create contextBase using above request
    //        Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
    //        httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object);

    //        // Set controllerContext
    //        controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
    //    }
    //}

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
                .ShouldRenderPartialView("_Success");
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
            controller.MakeAjaxRequest();

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
            controller.MakeAjaxRequest();
            controller
                .WithCallTo(c => c.UpdateUser(user.Id))
                .ShouldRenderPartialView("UpdateUser");
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
                Id = "123",
                UserType = UserType.User

            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                UserType = UserType.User
            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);

            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "User"));

            //Assert
            controller.MakeAjaxRequest();

            controller
                .WithCallTo(c => c.UpdateUser(user))
                .ShouldRedirectTo(x => x.Index());
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
                Id = "123",
                UserType = UserType.Admin
            };

            var viewModel = new UserViewModel
            {
                Id = "123",
                UserType = UserType.Admin

            };

            mockedMapper.Setup(x => x.Map<User>(viewModel)).Returns(user);

            mockedUsersService.Setup(c => c.Update(user));
            mockedProvider.Setup(x => x.RemoveFromRole(user.Id, "Admin"));

            //Assert
            controller.MakeAjaxRequest();
            controller
                .WithCallTo(c => c.UpdateUser(user))
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
                .WithCallTo(c => c.Index("Printer", printer.Id)).ShouldReturnJson();
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenEngravers_AreValid()
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
                .WithCallTo(c => c.Index("Engraver", engraver.Id)).ShouldReturnJson();
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenImpactPrinter_AreValid()
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
                .WithCallTo(c => c.Index("ImpactPrinter", impactPrinter.Id)).ShouldReturnJson();
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenVinylCutters_AreValid()
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
                .WithCallTo(c => c.Index("Cutter", cutter.Id)).ShouldReturnJson();
        }

        [Test]
        public void UpdateCutterPOST_ShouldReturnSuccess()
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
                .WithCallTo(c => c.UpdateCutter(cutter)).ShouldRenderPartialView("_Success");
        }

        [Test]
        public void UpdatePrinterPOST_ShouldReturnSuccess()
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
                .WithCallTo(c => c.UpdatePrinter(printer)).ShouldRenderPartialView("_Success");
        }

        [Test]
        public void UpdateEngraverPOST_ShouldReturnSuccess()
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
                .WithCallTo(c => c.UpdateEngraver(engraver)).ShouldRenderPartialView("_Success");
        }


        [Test]
        public void Index_ShouldReturnsTrue_WhenUsers_AreValid()
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

            var guid = new Guid();
            guid = Guid.Parse(user.Id);

            //Assert
            controller
                .WithCallTo(c => c.Index("User", guid)).ShouldReturnJson();
        }

        [Test]
        public void Printers_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var viewModel = new ProductViewModel();

            // Act & Assert
            controller.MakeAjaxRequest();
            
            controller.WithCallTo(c => c.Printers()).ShouldRenderPartialView("_Printers");
        }

        [Test]
        public void Engravers_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var viewModel = new ProductViewModel();

            // Act & Assert
            controller.MakeAjaxRequest();

            controller.WithCallTo(c => c.Engravers()).ShouldRenderPartialView("_Engravers");
        }

        [Test]
        public void Cutters_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var viewModel = new ProductViewModel();

            // Act & Assert
            controller.MakeAjaxRequest();

            controller.WithCallTo(c => c.Cutters()).ShouldRenderPartialView("_Cutters");
        }

        [Test]
        public void UpdateCutters_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var vinylCutter = new VinylCutter();

            var cuttersCollection = new List<VinylCutter>() { vinylCutter };
            
            mockedVinylCutterService.Setup(c => c.GetAll()).Returns(cuttersCollection.AsQueryable());


            // Act and Assert
            controller.MakeAjaxRequest();
            controller
                .WithCallTo(c => c.UpdateCutter(vinylCutter.Id))
                .ShouldRenderPartialView("_UpdateCutter");
        }

        [Test]
        public void UpdateEngraverGET_ShouldReturnsPartialView()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var engraver = new Engraver();

            var engraversCollection = new List<Engraver>() { engraver };

            mockedEngraverService.Setup(c => c.GetAll()).Returns(engraversCollection.AsQueryable());


            // Act and Assert
            controller.MakeAjaxRequest();
            controller
                .WithCallTo(c => c.UpdateEngraver(engraver.Id))
                .ShouldRenderPartialView("_UpdateEngraver");
        }

        [Test]
        public void UpdatePrinterGET_ShouldReturnsPartialView()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            var printer = new Printer();

            var printersCollection = new List<Printer>() { printer };

            mockedPrinterService.Setup(c => c.GetAll()).Returns(printersCollection.AsQueryable());


            // Act and Assert
            controller.MakeAjaxRequest();
            controller
                .WithCallTo(c => c.UpdatePrinter(printer.Id))
                .ShouldRenderPartialView("_UpdatePrinter");
        }

        [Test]
        public void AddPrinterGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddPrinter())
                .ShouldRenderPartialView("_AddPrinter");
        }

        [Test]
        public void AddEngraverGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddEngraver())
                .ShouldRenderPartialView("_AddEngraver");
        }

        [Test]
        public void AddCutterGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            var controller = new AdminController();

            // Act and Assert
            controller
                .WithCallTo(c => c.AddCutter())
                .ShouldRenderPartialView("_AddCutter");
        }

        [Test]
        public void AddComputerPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Printer, PrinterViewModel>();
                cfg.CreateMap<PrinterViewModel, Printer>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var printer = new Printer
            {
                Id = Guid.NewGuid()
            };

            mockedPrinterService.Setup(c => c.Add(printer));

            //Assert
            controller
                .WithCallTo(c => c.AddPrinter(printer))
                .ShouldRenderPartialView("_Success");
        }

        [Test]
        public void AddEngraverPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Engraver, EngraverViewModel>();
                cfg.CreateMap<EngraverViewModel, Engraver>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var engraver = new Engraver()
            {
                Id = Guid.NewGuid()
            };

            mockedEngraverService.Setup(c => c.Add(engraver));

            //Assert
            controller
                .WithCallTo(c => c.AddEngraver(engraver))
                .ShouldRenderPartialView("_Success");
        }

        [Test]
        public void AddCutterPOST_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VinylCutter, VinylCutterViewModel>();
                cfg.CreateMap<VinylCutterViewModel, VinylCutter>();
            });

            var controller = new AdminController(mockedHttpContext.Object, mockedProvider.Object, mockedUsersService.Object,
                mockedMapper.Object, mockedPrinterService.Object, mockedImpactPrinterService.Object, mockedEngraverService.Object, mockedVinylCutterService.Object);

            // Act
            var vinylCutter = new VinylCutter()
            {
                Id = Guid.NewGuid()
            };

            mockedVinylCutterService.Setup(c => c.Add(vinylCutter));

            //Assert
            controller
                .WithCallTo(c => c.AddCutter(vinylCutter))
                .ShouldRenderPartialView("_Success");
        }

        [Test]
        public void AdminAreaTest()
        {
            var adminArea = new AdminAreaRegistration();
            Assert.That(adminArea.AreaName == "Admin");
        }
    }
}
