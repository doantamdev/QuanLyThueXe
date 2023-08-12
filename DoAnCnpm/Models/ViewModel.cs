using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnCnpm.Models
{
    public class ViewModel
    {
        public string NamePro { get; set; }
        public string ImgPro { get; set; }
        public decimal pricePro { get; set; }
        public string NameCate { get; set; }
        public string DesPro { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int? IDPro { get; set; }
        public decimal Total_Money { get; set; }
        public Product product { get; set; }
        public Category category { get; set; }
        public OrderDetail orderDetail { get; set; }
        public int? Top3_Quantity { get; set; }
        public int? sum_Quantity { get; set; }
    }
}