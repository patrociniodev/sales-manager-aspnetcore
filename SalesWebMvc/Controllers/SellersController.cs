using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View(_sellerService.FindAll());
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() { Departments = departments };
            return View(viewModel);
        }

        //Annotations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //Remove -> método GET (chama a tela para confirmar a deleção)
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        } 

        //Remove -> método POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
