using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.DataAccess.Repository.IRepository;
using WebBanSach.Model;

namespace WebBanSach.DataAccess.Repository
{
    public class ProductRepository :  Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;
       
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
           
        }
        public void Update(Product product)
        {
            // Find the existing category in the database
            var objFromDb = _db.Products.FirstOrDefault(c => c.Id == product.Id);
            if (objFromDb != null)
            {
                // Update the properties of the existing category
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.CoverTypeId = product.CoverTypeId;
                // chỉ cập nhật ImageUrl nếu controller truyền lên giá trị (khi có upload)
                if (!string.IsNullOrWhiteSpace(product.ImageUrl))
                    objFromDb.ImageUrl = product.ImageUrl;

            }
            // Save changes to the database
            _db.Products.Update(product);
        }
        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
