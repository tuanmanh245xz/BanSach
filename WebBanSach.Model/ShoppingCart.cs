using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSach.Model
{
    public class ShoppingCart
    {
        public Product product { get; set; }
        [Range(1, 100, ErrorMessage = "Số lượng sản phẩm phải từ 1 đến 100")]
        public int Count { get; set; } // Số lượng sản phẩm trong giỏ hàng
    }
}
