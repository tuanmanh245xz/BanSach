using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSach.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }//
        //Thuoc tinh de lay ra CategoryRepository
        //IProductRepository Product { get; } //Neu co them ProductRepository va chi can goi la duoc
        ICoverTypeRepository CoverType { get; } //Thuoc tinh de lay ra CoverTypeRepository dang ky o day trc
        IProductRepository Product { get; } //Thuoc tinh de lay ra ProductRepository dang ky o day truoc
        void Save(); //Luu thay doi
    }
}
