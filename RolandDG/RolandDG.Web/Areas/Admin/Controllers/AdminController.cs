using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.ViewModels.Product;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Account;
using RolandDG.Web.ViewModels.Manage;

namespace RolandDG.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        private readonly IUsersService usersService;
        private readonly IHttpContextProvider httpContext;
        private readonly IVerificationProvider verification;
        private readonly IMapper mapper;
        private readonly IPrintersService printersService;
        private readonly IImpactPrintersService impactPrinterService;
        private readonly IEngraversService engraversService;
        private readonly IVinylCuttersService vinylCuttersService;

        public AdminController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        public AdminController(IHttpContextProvider httpContext, IVerificationProvider verification,
            IUsersService usersService, IMapper mapper, IPrintersService printersService, 
            IImpactPrintersService impactPrinterService, IEngraversService engraversService,
            IVinylCuttersService vinylCuttersService)
        {
            Guard.WhenArgument(httpContext, nameof(httpContext)).IsNull().Throw();
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(printersService, nameof(printersService)).IsNull().Throw();
            Guard.WhenArgument(impactPrinterService, nameof(impactPrinterService)).IsNull().Throw();
            Guard.WhenArgument(engraversService, nameof(engraversService)).IsNull().Throw();
            Guard.WhenArgument(vinylCuttersService, nameof(vinylCuttersService)).IsNull().Throw();

            this.usersService = usersService;
            this.mapper = mapper;
            this.printersService = printersService;
            this.impactPrinterService = impactPrinterService;
            this.engraversService = engraversService;
            this.vinylCuttersService = vinylCuttersService;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // USERS \\
        // Add User \\
        [HttpGet]
        public ActionResult AddUser()
        {
            ViewData["Title"] = "Register new user";
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddUser(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                var result = this.verification.Register(user, model.Password);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public ActionResult Users()
        {
            ViewData["Title"] = "Users";


            var users = this.usersService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<UserViewModel>()
                .ToList();

            users = users.Select(x =>
            {
                x.Role = this.verification
                .IsInRole(x.Id, "Admin") ? "Admin" : "User";
                return x;
            }).ToList();

            return PartialView(users);
        }

        // Update User \\
        [HttpGet]
        public ActionResult UpdateUser(string id)
        {
            ViewData["Title"] = "Update user profile";

            var user = this.usersService
                .GetAll()
                .ProjectTo<UserViewModel>()
                .Single(x => x.Id == id);

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserViewModel viewModel)
        {
            var role = viewModel.Role;
            var user = this.mapper.Map<User>(viewModel);

            user.UserName = user.Email;
            user.CreatedOn = user.CreatedOn;
            user.PasswordHash = user.PasswordHash;
            user.SecurityStamp = user.SecurityStamp;


            if (role == "User")
            {
                this.verification.AddToRole(user.Id, "User");
                this.verification.RemoveFromRole(user.Id, "Admin");
            }
            else if (role == "Admin")
            {
                this.verification.AddToRole(user.Id, "Admin");
                this.verification.RemoveFromRole(user.Id, "User");
            }

            this.usersService.Update(user);
            return RedirectToAction("Index", "Admin");
        }








        [HttpGet]
        public ActionResult AddPrinter()
        {
            return PartialView("_AddPrinter");
        }

        [HttpPost]
        public ActionResult AddPrinter(Printer printer)
        {
            var userId = User.Identity.GetUserId();
            //var currentUser = this.usersService.GetAll()
            //    .Single(x => x.Id == userId);

            printer.CreatedOn = DateTime.Now;
            //printer.Seller = currentUser;
            this.printersService.Add(printer);

            return PartialView("_Success");
        }

        // PRINTERS \\
        [HttpGet]
        public ActionResult Printers()
        {
            this.Request.IsAjaxRequest();
            ViewData["Title"] = "Printers";

            var computers = this.printersService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<PrinterViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                PrintersCollection = computers
            };

            return PartialView("_Printers", viewModel);
        }

        // Update Printer \\
        [HttpGet]
        public ActionResult UpdatePrinter(Guid id)
        {
            this.Request.IsAjaxRequest();

            var printer = this.printersService
                .GetAll()
                .ProjectTo<PrinterViewModel>()
                .Single(x => x.Id == id);

            return PartialView("_UpdatePrinter", printer);
        }

        [HttpPost]
        public ActionResult UpdatePrinter(Printer printer)
        {
            this.printersService.Update(printer);

            return PartialView("_Success");
        }

        //
        // ENGRAVERS \\
        //
        //
        // ADD ENGRAVER \\
        [HttpGet]
        public ActionResult AddEngraver()
        {
            return PartialView("_AddEngraver");
        }

        [HttpPost]
        public ActionResult AddEngraver(Engraver engraver)
        {
            var userId = User.Identity.GetUserId();
            //var currentUser = this.usersService.GetAll()
            //    .Single(x => x.Id == userId);

            engraver.CreatedOn = DateTime.Now;
            //printer.Seller = currentUser;
            this.engraversService.Add(engraver);

            return PartialView("_Success");
        }

        // Update ENGRAVERS \\
        [HttpGet]
        public ActionResult UpdateEngraver(Guid id)
        {
            this.Request.IsAjaxRequest();

            var engraver = this.engraversService
                .GetAll()
                .ProjectTo<EngraverViewModel>()
                .Single(x => x.Id == id);

            return PartialView("_UpdateEngraver", engraver);
        }

        [HttpPost]
        public ActionResult UpdateEngraver(Engraver engraver)
        {
            this.engraversService.Update(engraver);

            return PartialView("_Success");
        }

        // ENGRAVERS \\
        [HttpGet]
        public ActionResult Engravers()
        {
            this.Request.IsAjaxRequest();

            var engravers = this.engraversService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<EngraverViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                EngraversCollection = engravers
            };

            return PartialView("_Engravers", viewModel);
        }

        //===============================================================================
        //===============================================================================

        //
        // ENGRAVERS \\
        //
        //
        // ADD ENGRAVER \\
        [HttpGet]
        public ActionResult AddCutter()
        {
            return PartialView("_AddCutter");
        }

        [HttpPost]
        public ActionResult AddCutter(VinylCutter cutter)
        {
            var userId = User.Identity.GetUserId();
            //var currentUser = this.usersService.GetAll()
            //    .Single(x => x.Id == userId);

            cutter.CreatedOn = DateTime.Now;
            //printer.Seller = currentUser;
            this.vinylCuttersService.Add(cutter);

            return PartialView("_Success");
        }

        // Update ENGRAVERS \\
        [HttpGet]
        public ActionResult UpdateCutter(Guid id)
        {
            this.Request.IsAjaxRequest();

            var cutter = this.vinylCuttersService
                .GetAll()
                .ProjectTo<VinylCutterViewModel>()
                .Single(x => x.Id == id);

            return PartialView("_UpdateCutter", cutter);
        }

        [HttpPost]
        public ActionResult UpdateCutter(VinylCutter cutter)
        {
            this.vinylCuttersService.Update(cutter);

            return PartialView("_Success");
        }

        // ENGRAVERS \\
        [HttpGet]
        public ActionResult Cutters()
        {
            this.Request.IsAjaxRequest();

            var cutters = this.vinylCuttersService
                .GetAll()
                .OrderByDescending(x => x.ModifiedOn)
                .ProjectTo<VinylCutterViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                VinylCuttersCollection = cutters
            };

            return PartialView("_Cutters", viewModel);
        }
    }
}