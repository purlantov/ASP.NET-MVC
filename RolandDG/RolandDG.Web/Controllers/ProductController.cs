using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Roland.Data.Model;
using RolandDG.Services.Contracts;
using RolandDG.Web.ViewModels.Product;

namespace RolandDG.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPrintersService printersService;

        public ProductController(IMapper mapper, IPrintersService printersService)
        {
            this.mapper = mapper;
            this.printersService = printersService;
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


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}