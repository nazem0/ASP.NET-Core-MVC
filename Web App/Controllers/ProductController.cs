using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Repository_Pattern;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ViewModel;

namespace Web_App.Controllers
{
    public class ProductController : Controller
    {
        ProductManager ProductManager;
        UnitOfWork UnitOfWork;
        CategoryManager CategoryManager;
        public ProductController(
            ProductManager _ProductManager,
            UnitOfWork _UnitOfWork,
            CategoryManager _CategoryManager)
        {
            ProductManager = _ProductManager;
            UnitOfWork = _UnitOfWork;
            CategoryManager = _CategoryManager;
        }
        public IActionResult GetAll()
        {
            List<Product> ProductsList = ProductManager.Get().ToList();
            List<ProductViewModel> ProductsViewModelList = new List<ProductViewModel>();
            foreach (Product p in ProductsList)
            {
                ProductsViewModelList.Add(p.ToProductViewModel());
            }
            return View(ProductsViewModelList);
        }
        public IActionResult GetDetails(int ID)
        {
            return View(ProductManager.Get(ID));
        }
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Categories"] = CategoryManager.Get().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            }).ToList();

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(ProductViewModel P)
        {

            if (ModelState.IsValid)
            {
                foreach (IFormFile file in P.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    P.ImagesUrl.Add(file.FileName);
                }
            }
            ProductManager.Add(P.ToProduct());
            UnitOfWork.commit();
            return RedirectToAction("GetAll");

            //return RedirectToAction("Add");

        }

        public IActionResult Edit(int ID)
        {
            return View(ProductManager.Get(ID));
        }

        [HttpPost]
        public IActionResult Edit(Product P)
        {

            ProductManager.Edit(P, P.ID);
            UnitOfWork.commit();
            return RedirectToAction("GetAll");

            //return RedirectToAction("Add");

        }
        //[Route("remove/ID")]
        public IActionResult Remove(int ID)
        {
            ProductManager
                .Delete(ProductManager.Get(ID));
            UnitOfWork.commit();
            return RedirectToAction("GetAll");
        }
    }
}
