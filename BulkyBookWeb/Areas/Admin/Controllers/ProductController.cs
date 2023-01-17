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
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                product = new(),
                CategoryList = _unitOfWork.Categories.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverList = _unitOfWork.Covers.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                //Create product
                return View(productVM);   
            }
            else
            {
                //Edit Product
                productVM.product = _unitOfWork.Products.GetFirstOrDefault(p => p.Id == id);    
                return View(productVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.product.ImageUrl!=null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension),FileMode.Create))
                    {
                        file.CopyTo(fileStream);    
                    };
                    obj.product.ImageUrl = @"\Images\Products\" + filename + extension;
                }
                if(obj.product.Id == 0)
                {
                    _unitOfWork.Products.Add(obj.product);
                    TempData["success"] = "Product Created successfully";
                }
                else
                {
                    _unitOfWork.Products.Update(obj.product);
                    TempData["success"] = "Product Updated successfully";
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
            var productList = _unitOfWork.Products.GetAll(includeProperties: "Category,Cover");
            return Json(new { Data=productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Products.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return Json(new { success =  false, message = "Error while deleting" });
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Products.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
