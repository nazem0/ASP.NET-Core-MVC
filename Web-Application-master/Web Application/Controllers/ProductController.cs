﻿using Microsoft.AspNetCore.Mvc;

namespace Web_Application.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
