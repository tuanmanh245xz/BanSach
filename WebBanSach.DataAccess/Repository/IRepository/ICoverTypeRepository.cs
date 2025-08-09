 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.Model;

namespace WebBanSach.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType coverType);
        // Khong can phai khai bao Save o day vi da co trong IRepository
        //void Save(); // Khong can khai bao lai, da co trong IRepository
    }
}
