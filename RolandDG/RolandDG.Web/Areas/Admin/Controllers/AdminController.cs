using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using RolandDG.Services.Contracts;

namespace RolandDG.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private ApplicationUserManager _userManager;

        //private readonly IMapper mapper;
        //private readonly IProdictsService productService;

        //public AdminController(ApplicationUserManager userManager)
        //{
        //    this.UserManager = userManager;
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

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


    }
}