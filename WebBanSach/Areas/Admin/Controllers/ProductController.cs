
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            var count = Request.Form.Files.Count;
            if (ModelState.IsValid)
            {
                // upload images
                string wwwRootPath = _webHostEnvironment.WebRootPath;
           
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"images\products\" + fileName + extension;

                }
                if (obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }

                _unitOfWork.Save();
                TempData["Sucess"] = "Product create Sucessfully";
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
        #region Api
        // API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includePr: "Category,CoverType");
            return Json(new { data = productList });
        }
        // post
        [HttpDelete]
        public IActionResult DeletePostApi(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                if (obj.ImageUrl != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful" });
            }


            return View(obj);
        }
        #endregion
    }
}
