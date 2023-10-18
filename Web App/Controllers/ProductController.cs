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
        public IActionResult GetAll(int page = 0)
        {
            List<Product> ProductsList = new List<Product> { };
            if (page < 1)
            {
                page = 1;
            }


            int PageSize = 2;
            ProductsList = ProductManager.Get()
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            List<ProductViewModel> ProductsViewModelList = new List<ProductViewModel>();
            foreach (Product p in ProductsList)
            {
                ProductsViewModelList.Add(p.ToProductViewModel());
            }
            return View(ProductsViewModelList);
        }
        public IActionResult Search(string search, string category, int page = 0)
        {
            if (page < 1)
            {
                page = 1;
            }
            int PageSize = 2;
            List<Product> ProductsList = new List<Product> { };
            IQueryable<Product> Filteration = ProductManager.Get();
            if (!string.IsNullOrEmpty(category))
            {
                Filteration = Filteration
                .Where(
                    i => i.Category.Name
                        .ToLower()
                        .Contains(category)
                    );

            }
            if (!string.IsNullOrEmpty(search))
            {
                Filteration = Filteration
                .Where(
                    i => i.Name
                        .ToLower()
                        .Contains(search)
                    );
            }

            int Count = Filteration.Count();
            ProductsList = Filteration
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize).ToList();


            List<ProductViewModel> ProductsViewModelList = new List<ProductViewModel>();
            foreach (Product p in ProductsList)
            {
                ProductsViewModelList.Add(p.ToProductViewModel());
            }
            return View("GetAll", ProductsViewModelList);
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
