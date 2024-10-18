using App.Application.Features.Products.Dto;

namespace App.Application.Features.Categories.Dto;

public record CategoryWithProductsDto(int ID, string Name, List<ProductDto> Products);
