
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanSach.DataAccess.Repository.IRepository;
using WebBanSach.Model;
using WebBanSach.Model.ViewModel;

namespace WebBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductTypeList = _unitOfWork.Product.GetAll();
            return View(objProductTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "product Create Sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Upsert(int? id)
        {



            ProductVM productVM = new ProductVM();
            productVM.product = new Product(); // Initialize the product property
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            productVM.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            if (id == null || id == 0)
            {
                // create product
                return View(productVM);
            }
            else
            {
                // update product
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                if (productVM.product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Product Update Sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ProductfromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
           
            if (ProductfromDb == null)
            {
                return NotFound();
            }

            return View(ProductfromDb);
        }

        // post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
          
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Covert Type Delete Sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

    }
}
