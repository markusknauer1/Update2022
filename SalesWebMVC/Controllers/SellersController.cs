using System;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
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

        // IMPRESSÃO DA LISTA DE USUARIOS CADASTRADOS
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //  SOLICITAÇÃO DE CADASTRO
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // METODO POST  =========================================== CADASTRAR NOVO USUARIO E VALIDAR CAMPOS
        [HttpPost]
        //evita ataque de CSRF
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);// METODO QUE EVITA QUE O NAVEGADOR GRAVE QLQR CADASTRO COM O JAVASCRIPT DESATIVADO
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "ID não detectado"});
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }

            return View(obj);
        }
        // METODO GET ================================== CONSULTAR EXISTENCIA DE USUÁRIO PELO ID E IXIBIR
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não detectado!" });
            }
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }


        //  METODO POST ================================= ATUALIZAR USUÁRIO E VALIDAR CAMPOS DE USUÁRIO EXISTENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);// METODO QUE EVITA QUE O NAVEGADOR GRAVE QLQR CADASTRO COM O JAVASCRIPT DESATIVADO
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Não corresponde!" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }




        //METODO POST =========================================== DELETAR CADASTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não detectado" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }

            return View(obj);
        }
    }
}
