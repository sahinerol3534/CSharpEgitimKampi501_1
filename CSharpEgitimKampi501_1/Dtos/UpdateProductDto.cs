using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampi501_1.Dtos
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        public string ProductCatagory { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
