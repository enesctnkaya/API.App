using App.Services.Products;

namespace App.Services.Categories.Dto
{
    public record CategoryWithProductsDto(int ID, string Name, List<ProductDto> Products);
}
