using System;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // METODO GET =========================================== CADASTR0 NOVO USUARIO E VALIDAR CAMPOS
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // METODO POST  ========================================= CADASTRAR NOVO USUARIO E VALIDAR CAMPOS
        [HttpPost]
        //evita ataque de CSRF
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel); // METODO QUE EVITA QUE O NAVEGADOR GRAVE QLQR CADASTRO COM O JAVASCRIPT DESATIVADO
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // ===============================================================================================================

        // METODO GET =========================================== DETALHES DO CADASTRO
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não detectado" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }

            return View(obj);
        }

        // ===============================================================================================================
        
        // METODO GET =========================================== CONSULTAR EXISTENCIA DE USUÁRIO PELO ID E IXIBIR
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não detectado!" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }
        // METODO POST ========================================== CONSULTAR EXISTENCIA DE USUÁRIO PELO ID E IXIBIR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);// METODO QUE EVITA QUE O NAVEGADOR GRAVE QLQR CADASTRO COM O JAVASCRIPT DESATIVADO
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Não corresponde!" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller); // atualizar no banco
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


        // ===============================================================================================================
        
        // METODO GET =========================================== DELETAR CADASTRO
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não detectado" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe o ID!" });
            }

            return View(obj);
        }

        //METODO POST =========================================== DELETAR CADASTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
