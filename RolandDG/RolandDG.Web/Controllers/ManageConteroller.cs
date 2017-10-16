using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RolandDG.Services.Contracts;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Manage;

namespace RolandDG.Web.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ManageController : Controller
    {
        private readonly IVerificationProvider verification;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        //private readonly IComputersService computersService;
        //private readonly ILaptopsService laptopsService;
        //private readonly IDisplaysService displaysService;

        public ManageController()
        {
        }
        public ManageController(IVerificationProvider verification, IMapper mapper, IUsersService usersService)
        {
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            //Guard.WhenArgument(computersService, nameof(computersService)).IsNull().Throw();
            //Guard.WhenArgument(laptopsService, nameof(laptopsService)).IsNull().Throw();
            //Guard.WhenArgument(displaysService, nameof(displaysService)).IsNull().Throw();

            this.verification = verification;
            this.mapper = mapper;
            this.usersService = usersService;
            //this.computersService = computersService;
            //this.laptopsService = laptopsService;
            //this.displaysService = displaysService;
        }

        // USER PROFILE \\
        // GET: /Manage/Index
        [HttpGet]
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            ViewData["Title"] = "Manage";

            var userId = this.verification.CurrentUserId;

            //var computers = this.computersService
            //    .GetAll()
            //    .Where(x => x.Seller.Id == userId)
            //    .ProjectTo<ComputerViewModel>()
            //    .ToList();

            //var laptops = this.laptopsService
            //    .GetAll()
            //    .Where(x => x.Seller.Id == userId)
            //    .ProjectTo<LaptopViewModel>()
            //    .ToList();

            //var displays = this.displaysService
            //    .GetAll()
            //    .Where(x => x.Seller.Id == userId)
            //    .ProjectTo<DisplayViewModel>()
            //    .ToList();

            //var viewModel = new List<IDeviceViewModel>();
            //viewModel.AddRange(computers);
            //viewModel.AddRange(laptops);
            //viewModel.AddRange(displays);

            //viewModel = viewModel.OrderByDescending(x => x.PostedOn).ToList();

            //var pageNumber = page ?? 1;
            //var pageSize = 10;

            //return View(viewModel.ToPagedList(pageNumber, pageSize));
            return View();
        }

        // Delete Advertisement \\
        [HttpPost]
        public ActionResult Index(string type, Guid id)
        {
            switch (type)
            {
                //case "Computer":
                //    var computer = this.computersService.GetAll().Single(x => x.Id == id);
                //    this.computersService.Delete(computer); break;
                //case "Laptop":
                //    var laptop = this.laptopsService.GetAll().Single(x => x.Id == id);
                //    this.laptopsService.Delete(laptop); break;
                //case "Display":
                //    var display = this.displaysService.GetAll().Single(x => x.Id == id);
                //    this.displaysService.Delete(display); break;
            }
            return Json(null);
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            ViewData["Title"] = "Change Password";
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = this.verification.ChangePassword(this.verification.CurrentUserId, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = this.verification.GetUserById(this.verification.CurrentUserId);
                if (user != null)
                {
                    this.verification.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            AddErrors(result);
            return View(model);
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}