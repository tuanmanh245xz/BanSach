using Microsoft.AspNetCore.Mvc;
using WebBanSach.DataAccess;
using WebBanSach.DataAccess.Repository.IRepository;
using WebBanSach.Model;
namespace WebBanSach.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uniWork;
        public CategoryController(IUnitOfWork db)
        {
            _uniWork = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _uniWork.Category.GetAll();
            return View(categories);
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //trong gia mao
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _uniWork.Category.Add(category); //add 
                _uniWork.Save();//tu dong them
                TempData["success"] = "Category created successfully"; //thong bao thanh cong
                return RedirectToAction("Index");
            }
                return View(category);

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _uniWork.Category.GetFirstOrDefault(u => u.Id == id);//tim phan tu theo id
            //var category = _db.Categories.FirstOrDefault(u => u.Id == id); tim phan tu dau tien
            //var category = _db.Categories.SingleDefault(u => u.Id == id); tu tim phan tu duy nhat
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //trong gia mao
        public IActionResult Edit(Category category )
        {


            _uniWork.Category.Update(category); //add 
            _uniWork.Save();//tu dong them
                TempData["success"] = "Edit successfully"; //thong bao thanh cong
                return RedirectToAction("Index");
            
            

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _uniWork.Category.GetFirstOrDefault(u => u.Id == id);//tim phan tu theo id
            //var category = _db.Categories.FirstOrDefault(u => u.Id == id); tim phan tu dau tien
            //var category = _db.Categories.SingleDefault(u => u.Id == id); tu tim phan tu duy nhat
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //trong gia mao
        public IActionResult Delete(Category category)
        {
           
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _uniWork.Category.Remove(category); //add 
                _uniWork.Save();//tu dong them
                TempData["success"] = "Delete successfully"; //thong bao thanh cong
                return RedirectToAction("Index");
            }
          
        }
    }
}
