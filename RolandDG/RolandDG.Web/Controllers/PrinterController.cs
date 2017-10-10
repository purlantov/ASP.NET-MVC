using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RolandDG.Services.Contracts;
using Roland_ASP_MVC.ViewModels.Product;

namespace Roland_ASP_MVC.Controllers
{
    public class PrinterController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPrintersService printersService;

        public PrinterController(IMapper mapper, IPrintersService printersService)
        {
            this.mapper = mapper;
            this.printersService = printersService;
        }


        [HttpGet]
        public ActionResult Printer()
        {
            ViewData["Title"] = "Printer";

            var printer = printersService
                .GetAll()
                .ProjectTo<PrinterViewModel>()
                .ToList();

            //var printerViewModel = Mapper.Map<PrinterViewModel>(printer);

            return View(printer);
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}