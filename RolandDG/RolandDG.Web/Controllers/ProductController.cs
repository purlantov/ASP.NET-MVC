using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using PagedList;
using Roland.Data.Model;
using RolandDG.Services;
using RolandDG.Services.Contracts;
using RolandDG.Web.Providers.Contracts;
using RolandDG.Web.ViewModels.Product;

namespace RolandDG.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IVerificationProvider verification;
        private readonly IMapper mapper;
        private readonly IPrintersService printersService;
        private readonly IImpactPrintersService impactPrintersService;
        private readonly IEngraversService engraversService;
        private readonly IVinylCuttersService vinylCuttersService;

        public ProductController(IMapper mapper, IPrintersService printersService,
            IImpactPrintersService impactPrinterService, IEngraversService engraversService,
            IVinylCuttersService vinylCuttersService, IVerificationProvider verification)
        {
            Guard.WhenArgument(verification, nameof(verification)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(printersService, nameof(printersService)).IsNull().Throw();
            Guard.WhenArgument(impactPrinterService, nameof(impactPrinterService)).IsNull().Throw();
            Guard.WhenArgument(engraversService, nameof(engraversService)).IsNull().Throw();
            Guard.WhenArgument(vinylCuttersService, nameof(vinylCuttersService)).IsNull().Throw();

            this.mapper = mapper;
            this.printersService = printersService;
            this.impactPrintersService = impactPrinterService;
            this.engraversService = engraversService;
            this.vinylCuttersService = vinylCuttersService;
            this.verification = verification;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
         
        [HttpGet]
        public ActionResult Printer(Guid Id)
        {
            ViewData["Title"] = "Printer";

            var printer = printersService
                .GetAll()
                .ProjectTo<PrinterViewModel>()
                .Single(x => x.Id == Id);

            //var printerViewModel = mapper.Map<PrinterViewModel>(printer);

            return View(printer);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Printers(int? page)
        {
            ViewData["Title"] = "Printers";

            var printers = printersService
                .GetAll()
                .ProjectTo<PrinterViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                PrintersCollection = printers
            };

            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(viewModel.PrintersCollection.ToPagedList(pageNumber, pageSize));
        }

        //[HttpGet]
        //[Authorize(Roles = "User, Admin")]
        //[Route("add-printer")]
        //public ActionResult AddPrinter()
        //{
        //    ViewData["Title"] = "Create Printer";
        //    return View();
        //}

        //[HttpPost]
        //[Authorize(Roles = "User, Admin")]
        //[ValidateAntiForgeryToken]
        //[Route("add-printer")]
        //public ActionResult AddPrinter(Printer printer)
        //{
        //    var userId = User.Identity.GetUserId();
        //    //var currentUser = this.usersService.GetAll()
        //    //    .Single(x => x.Id == userId);

        //    printer.CreatedOn = DateTime.Now;
        //    //printer.Seller = currentUser;
        //    this.printersService.Add(printer);

        //    return RedirectToAction("Printers", "Product");
        //}


        [HttpGet]
        //[OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Engravers(int? page)
        {
            ViewData["Title"] = "Engravers";

            var engravers = engraversService
                .GetAll()
                .ProjectTo<EngraverViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                EngraversCollection = engravers
            };

            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(viewModel.EngraversCollection.ToPagedList(pageNumber, pageSize));

            //return View(engravers);
        }

        [HttpGet]
        public ActionResult Engraver(Guid Id)
        {
            ViewData["Title"] = "Engraver";

            var engraver = engraversService
                .GetAll()
                .ProjectTo<EngraverViewModel>()
                .Single(x => x.Id == Id);

            //var printerViewModel = mapper.Map<PrinterViewModel>(printer);

            return View(engraver);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult Cutters(int? page)
        {
            ViewData["Title"] = "Cutters";

            var cutters = vinylCuttersService
                .GetAll()
                .ProjectTo<VinylCutterViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                VinylCuttersCollection = cutters
            };

            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(viewModel.VinylCuttersCollection.ToPagedList(pageNumber, pageSize));

            //return View(cutters);
        }

        [HttpGet]
        public ActionResult Cutter(Guid Id)
        {
            ViewData["Title"] = "Cutter";

            var cutter = vinylCuttersService
                .GetAll()
                .ProjectTo<VinylCutterViewModel>()
                .Single(x => x.Id == Id);

            return View(cutter);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "ShortLived")]
        public ActionResult ImpactPrinters(int? page)
        {
            ViewData["Title"] = "ImpactPrinters";

            var impactPrinters = impactPrintersService
                .GetAll()
                .ProjectTo<ImpactPrinterViewModel>()
                .ToList();

            var viewModel = new ProductViewModel()
            {
                ImpactPrintersCollection = impactPrinters
            };

            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(viewModel.ImpactPrintersCollection.ToPagedList(pageNumber, pageSize));
            //return View(impactPrinters);
        }

        [HttpGet]
        public ActionResult ImpactPrinter(Guid Id)
        {
            ViewData["Title"] = "ImpactPrinter";

            var impactPrinter = impactPrintersService
                .GetAll()
                .ProjectTo<ImpactPrinterViewModel>()
                .Single(x => x.Id == Id);

            return View(impactPrinter);
        }
    }
}