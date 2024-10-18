using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{

    public record ProductDto(int ID, string Name,decimal Price, int Stock, int CategoryID);

    //public record ProductDto
    //{
    //    public int ID { get; init; }
    //    public string Name { get; init; }
    //    public decimal Price { get; init; }
    //    public int Stock {  get; init; }
    //}
}
