using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Modals;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var covers = _unitOfWork.Covers.GetAll();
            return View(covers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cover obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Covers.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var cover = _unitOfWork.Covers.GetFirstOrDefault(c => c.Id == id);
            if (cover == null)
                return NotFound();
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cover obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Covers.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var cover = _unitOfWork.Covers.GetFirstOrDefault(c => c.Id == id);
            if (cover == null)
                return NotFound();
            return View(cover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Covers.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Covers.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
