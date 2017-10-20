using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.Controllers;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Manage;
using TestStack.FluentMVCTesting;

namespace RolandDG.Tests.Controllers
{
    [TestFixture]
    public class ManageControllerTests
    {
        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenProviderIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();


            Assert.Throws<ArgumentNullException>(() =>
                new ManageController(null, mockedMapper.Object, mockedUsersService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();


            Assert.Throws<ArgumentNullException>(() =>
                new ManageController(mockedProvider.Object, null, mockedUsersService.Object));
        }

        [Test]
        public void Controller_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            //Arrange
            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();


            Assert.Throws<ArgumentNullException>(() =>
                new ManageController(mockedProvider.Object, mockedMapper.Object, null));
        }

        [Test]
        public void Index_ShouldReturnsTrue_WhenViewResult_IsValid()
        {
            var id = "123";

            // Arrange

            var mockedProvider = new Mock<IVerificationProvider>();
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();

            var controller =
                new ManageController(mockedProvider.Object, mockedMapper.Object, mockedUsersService.Object);

            //Act

            mockedProvider.Setup(v => v.CurrentUserId).Returns(id);


            //Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        //[Test]
        //public void ChangePasswordGET_ShouldReturnsTrue_WhenViewResult_IsValid()
        //{
        //    //Arrange
        //    var controller = new ManageController();
        //    var model = new ChangePasswordViewModel();


        //    //Assert
        //    controller
        //        .WithCallTo(c => c.Index(model))
        //        .ShouldRenderDefaultView();
        //}

        [Test]
        public void ChangePasswordPOST_ShouldRenderView_WhenIsNotValid()
        {
            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            var controller = new ManageController();
            controller.ModelState.AddModelError("wrorng model", "Error");
            var viewModel = new ChangePasswordViewModel();

            // Act and Assert
            controller
                .WithCallTo(c => c.Index(viewModel))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ChangePasswordPOST_ShouldCallVerificationChangePassword_WhenIsValid()
        {
            var id = "123";

            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            mockedVerification.Setup(v => v.CurrentUserId).Returns(id);
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();

            var controller =
                new ManageController(mockedVerification.Object, mockedMapper.Object, mockedUsersService.Object);


            var viewModel = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };
            var user = new Mock<User>();
            user.Object.Id = id;
            mockedVerification.Setup(x => x.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword)).Returns(IdentityResult.Success);

            controller.Index(viewModel);

            mockedVerification.Verify(v => v.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword), Times.Once);

        }

        [Test]
        public void ChangePasswordPOST_ShouldRedirects_WhenIsValid()
        {
            var id = "123";

            // Arrange
            var mockedVerification = new Mock<IVerificationProvider>();
            mockedVerification.Setup(v => v.CurrentUserId).Returns(id);
            var mockedMapper = new Mock<IMapper>();
            var mockedUsersService = new Mock<IUsersService>();

            var controller =
                new ManageController(mockedVerification.Object, mockedMapper.Object, mockedUsersService.Object);

            var viewModel = new ChangePasswordViewModel
            {
                OldPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            mockedVerification.Setup(x => x.ChangePassword(id, viewModel.OldPassword, viewModel.NewPassword)).Returns(IdentityResult.Success);

            // Act and Assert
            controller
                .WithCallTo(c => c.Index(viewModel))
                .ShouldRenderPartialView("_Success");
        }
    }
}
