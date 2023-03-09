using DiveInColors.DataAccess;
using DiveInColors.DataAccess.Repository.IRepository;
using DiveInColors.Models;
using DiveInColors.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoDiveInColors.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= SD.Role_User_InternalUser_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryfromDB = _db.Categories.Find(id);
            var categoryfromDBFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (categoryfromDBFirst == null)
            {
                return NotFound();
            }

            return View(categoryfromDBFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryfromDB = _db.Categories.Find(id);
            var categoryfromDBFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (categoryfromDBFirst == null)
            {
                return NotFound();
            }

            return View(categoryfromDBFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
