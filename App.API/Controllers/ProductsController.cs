﻿using App.Repositories.Products;
using App.Services.Filters;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => 
            CreateActionResult(await productService.GetAllListAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => 
            CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id) => 
            CreateActionResult(await productService.GetByIDAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) => 
            CreateActionResult(await productService.CreateAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPut("{id:int}")] // tam güncellemeyi temsil eder
        public async Task<IActionResult> Update(int id, UpdateProductRequest request) => 
            CreateActionResult(await productService.UpdateAsync(id, request));

        [HttpPatch("Stock")] // kısmi güncellemeyi temsil eder
        public async Task<IActionResult> UpdateSock(UpdateProductStockRequest request) =>
            CreateActionResult(await productService.UpdateStockAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => 
            CreateActionResult(await productService.DeleteAsync(id));
    }
}