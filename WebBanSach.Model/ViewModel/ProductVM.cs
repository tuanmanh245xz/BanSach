using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSach.Model.ViewModel
{
    public class ProductVM
    {
        public Product product  { get ; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; } = Enumerable.Empty<SelectListItem>();
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; } =  Enumerable.Empty<SelectListItem>();

       

    }
}
