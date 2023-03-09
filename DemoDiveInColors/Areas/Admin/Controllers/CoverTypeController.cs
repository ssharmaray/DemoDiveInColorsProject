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
	[Authorize(Roles = SD.Role_User_InternalUser_Admin)]
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

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created Successfully!";
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
            var coverTypefromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (coverTypefromDBFirst == null)
            {
                return NotFound();
            }

            return View(coverTypefromDBFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Edited Successfully!";
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
            var coverTypefromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (coverTypefromDBFirst == null)
            {
                return NotFound();
            }

            return View(coverTypefromDBFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryfromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryfromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
