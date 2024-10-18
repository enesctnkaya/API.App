using App.Repositories.Categories;

namespace App.Repositories.Products
{
    public class Product : BaseEntity<int> , IAuditEntity //propertys 
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
//int ID, string Name,decimal Price, int Stock