using DiveInColors.DataAccess;
using DiveInColors.DataAccess.Repository;
using DiveInColors.DataAccess.Repository.IRepository;
using DiveInColors.Models;
using DiveInColors.Models.ViewModels;
using DiveInColors.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoDiveInColors.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_User_InternalUser_Admin)]
	public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Company> objCompanyList = _unitOfWork.Company.GetAll();
            return View(objCompanyList);
            //return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company objCompany = new();
            
            if (id == null || id == 0)
            {
                return View(objCompany);
            }
            else
            {
                objCompany = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(objCompany);
            }

        }   

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Id==0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created Successfully!";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company Updated Successfully!";
                }
              
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj);
        }
                        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new {data= companyList});
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }


        #endregion
    }


}
