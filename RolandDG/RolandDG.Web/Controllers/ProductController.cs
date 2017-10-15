using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Roland.Data.Model;
using RolandDG.Services;
using RolandDG.Services.Contracts;
using RolandDG.Web.ViewModels.Product;

namespace RolandDG.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPrintersService printersService;
        private readonly IImpactPrintersService impactPrintersService;
        private readonly IEngraversService engraversService;
        private readonly IVinylCuttersService vinylCuttersService;

        public ProductController(IMapper mapper, IPrintersService printersService,
            IImpactPrintersService impactPrinterService, IEngraversService engraversService,
            IVinylCuttersService vinylCuttersService)
        {
            this.mapper = mapper;
            this.printersService = printersService;
            this.impactPrintersService = impactPrinterService;
            this.engraversService = engraversService;
            this.vinylCuttersService = vinylCuttersService;
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
        public ActionResult Printers()
        {
            ViewData["Title"] = "Printers";

            var printers = printersService
                .GetAll()
                .ProjectTo<PrinterViewModel>()
                .ToList();

            return View(printers);
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("add-printer")]
        public ActionResult AddPrinter()
        {
            ViewData["Title"] = "Create Printer";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [ValidateAntiForgeryToken]
        [Route("add-printer")]
        public ActionResult AddPrinter(Printer printer)
        {
            var userId = User.Identity.GetUserId();
            //var currentUser = this.usersService.GetAll()
            //    .Single(x => x.Id == userId);

            printer.CreatedOn = DateTime.Now;
            //printer.Seller = currentUser;
            this.printersService.Add(printer);

            return RedirectToAction("Printers", "Product");
        }


        [HttpGet]
        public ActionResult Engravers()
        {
            ViewData["Title"] = "Engravers";

            var engravers = engraversService
                .GetAll()
                .ProjectTo<EngraverViewModel>()
                .ToList();

            return View(engravers);
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
        public ActionResult Cutters()
        {
            ViewData["Title"] = "Cutters";

            var cutters = vinylCuttersService
                .GetAll()
                .ProjectTo<VinylCutterViewModel>()
                .ToList();

            return View(cutters);
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
        public ActionResult ImpactPrinters()
        {
            ViewData["Title"] = "ImpactPrinters";

            var impactPrinters = impactPrintersService
                .GetAll()
                .ProjectTo<ImpactPrinterViewModel>()
                .ToList();

            return View(impactPrinters);
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