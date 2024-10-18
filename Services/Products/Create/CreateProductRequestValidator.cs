using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Create
{
    public class createProductRequestValidator : AbstractValidator<CreateProductRequest>
    {

        private readonly IProductRepository _productRepository;
        public createProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi giriniz.")
                .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");
            // .Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bbulunmaktadır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
            
            RuleFor(x => x.CategoryID)
                .GreaterThan(0).WithMessage("Ürün kategori değeri 0'dan büyük olmalıdır.");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");
        }

        //private bool MustUniqueProductName(string name)
        //{

        //    return !_productRepository.Where(x => x.Name == name).Any();


        //    //false => bir hata var
        //    //true => bir hata yok
        //}
    }
}
