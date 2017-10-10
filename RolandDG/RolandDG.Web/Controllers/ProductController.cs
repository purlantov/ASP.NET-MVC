using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RolandDG.Services.Contracts;
using Roland_ASP_MVC.ViewModels.Product;

namespace Roland_ASP_MVC.Controllers
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

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}