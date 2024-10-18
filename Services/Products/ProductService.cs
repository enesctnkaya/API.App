using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProductRequest> createProductRequestValidator,
        IMapper mapper) : IProductService
    {

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {

            var products = await productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>()
            {
                Data = productsAsDto
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {

            var products = await productRepository.GetAll().ToListAsync();


            //var productsAsDto = products.Select(p => new ProductDto(p.ID, p.Name, p.Price,p.Stock)).ToList();

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();


            //var productsAsDto = products.Select(p => new ProductDto(p.ID, p.Name, p.Price, p.Stock)).ToList();

            var productsAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIDAsync(int id)
        {
            var product = await productRepository.GetByIDAsync(id);


            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            //var productAsDto = new ProductDto(product!.ID, product.Name, product.Price, product.Stock);

            var productsAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productsAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Ürün ismi zaten veri tabanında bulunmaktadır",
                    HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.ID),
                $"api/products/{product.ID}"
                );
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            //fast fail ilk olumsuz durumlar sonra olumlu durum
            //Guard Clauses ilk başta ifle olumsuz durumları yaz else kullanma


            var isProductNameExist =
                await productRepository.Where(x => x.Name == request.Name && x.ID != id).AnyAsync();
            if (isProductNameExist)
            {
                return ServiceResult.Fail("ürün ismi veritabanında bulunmaktadır.",
                    HttpStatusCode.BadRequest);
            }

            //product.Name = request.Name;
            //product.Price = request.Price;
            //product.Stock = request.Stock;

            var product = mapper.Map<Product>(request);
            product.ID = id;    

            product = mapper.Map(request, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)//quantity = miktar                                                                            // ürün deposu için async yani verilen task tamamlanmadan diğer işlemlerin devam etmesini sağlar ve awaitte bu işlemlerin tamamlanmasını bekler
        {
            var product = await productRepository.GetByIDAsync(request.ProductID);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIDAsync(id);

            productRepository.Delete(product!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}