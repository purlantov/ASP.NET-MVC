using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.ViewModels.Product;

namespace RolandDG.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;


        private readonly IMapper mapper;
        private readonly IPrintersService printersService;
        private readonly IImpactPrintersService impactPrinterService;
        private readonly IEngraversService engraversService;
        private readonly IVinylCuttersService vinylCuttersService;

        public AdminController(ApplicationUserManager userManager)
        {
            this.UserManager = userManager;
        }

        public AdminController(IMapper mapper, IPrintersService printersService,
            IImpactPrintersService impactPrinterService, IEngraversService engraversService,
            IVinylCuttersService vinylCuttersService)
        {
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

        //[HttpGet]
        //public ActionResult Products()
        //{
        //    ViewData["Title"] = "Products";


        //    //var product = this.
        //    //    .GetAll()
        //    //    .OrderByDescending(x => x.ModifiedOn)
        //    //    .ProjectTo<UserViewModel>()
        //    //    .ToList();


        //    //users = users.Select(x =>
        //    //{
        //    //    x.Role = this.UserManager
        //    //        .IsInRole(x.Id, "Admin") ? "Admin" : "User";
        //    //    return x;
        //    //}).ToList();


        //    //return PartialView(users);
        //}

        //public ActionResult AddProduct()
        //{
        //    return PartialView("_AddPrinter");
        //}

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

            return PartialView("_AddPrinter");
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

            return RedirectToAction("Index", "Admin");
        }


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

            return PartialView("_AddEngraver");
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
    }
}