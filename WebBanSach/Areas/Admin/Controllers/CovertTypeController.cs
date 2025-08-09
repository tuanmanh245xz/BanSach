
using Microsoft.AspNetCore.Mvc;
using WebBanSach.DataAccess.Repository.IRepository;
using WebBanSach.Model;

namespace WebBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "CoverType Create Sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }  
            
            var covertTypefromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
          
            if (covertTypefromDbFirst == null)
            {
                return NotFound();
            }

            return View(covertTypefromDbFirst);
        }

        // post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Cover Type Update Sucessfully";
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
            var coverTypefromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
           
            if (coverTypefromDb == null)
            {
                return NotFound();
            }

            return View(coverTypefromDb);
        }

        // post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
          
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.CoverType.Remove(obj);
                _unitOfWork.Save();
                TempData["Sucess"] = "Covert Type Delete Sucessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

    }
}
