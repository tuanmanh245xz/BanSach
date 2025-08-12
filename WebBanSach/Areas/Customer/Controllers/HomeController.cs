
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebBanSach.DataAccess.Repository.IRepository;
using WebBanSach.Model;

namespace WebBanSach.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable < Product > ProductList= _unitOfWork.Product.GetAll(includePr:"Category,CoverType");
            return View(ProductList);
        }

        public IActionResult Details(int productId)
        {

            ShoppingCart cartObj = new()
            {
                product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includePr: "Category,CoverType"),
               
            };

            return View(cartObj);
        }
      

      
    }
}