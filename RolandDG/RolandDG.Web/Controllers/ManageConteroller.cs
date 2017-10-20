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


        public ManageController()
        {
        }
        public ManageController(IVerificationProvider verification, IMapper mapper, IUsersService usersService)
        {
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();


            this.verification = verification;
            this.mapper = mapper;
            this.usersService = usersService;
   
        }

        // USER PROFILE \\
        // GET: /Manage/Index
        [HttpGet]
        public ActionResult Index()
        {
            //ViewBag.StatusMessage =
            //    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
            //    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
            //    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
            //    : message == ManageMessageId.Error ? "An error has occurred."
            //    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
            //    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
            //    : "";

            ViewData["Title"] = "Change Password";
            return View();
        }

        // Delete Advertisement \\
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = this.verification.ChangePassword(this.verification.CurrentUserId, model.OldPassword,
                model.NewPassword);

            if (result.Succeeded)
            {
                var user = this.verification.GetUserById(this.verification.CurrentUserId);
                if (user != null)
                {
                    this.verification.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                //return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
                return PartialView("_Success", "Admin");
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