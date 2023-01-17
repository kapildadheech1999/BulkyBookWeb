using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Modals;
using BulkyBook.Modals.ViewModel;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
                //Create Company
                return View(company);   
            }
            else
            {
                //Edit Company
                company = _unitOfWork.Companies.GetFirstOrDefault(p => p.Id == id);    
                return View(company);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Id == 0)
                {
                    _unitOfWork.Companies.Add(obj);
                    TempData["success"] = "Company Created successfully";
                }
                else
                {
                    _unitOfWork.Companies.Update(obj);
                    TempData["success"] = "Company Updated successfully";
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
            var companyList = _unitOfWork.Companies.GetAll();
            return Json(new { Data=companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Companies.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success =  false, message = "Error while deleting" });
            }
            
            _unitOfWork.Companies.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
