using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface ILoginRepository:IDisposable
    {
        IEnumerable<AdminLogin> GetAll();
        AdminLogin GetAdminByID(int AdminID);
        bool InsertAdmin(AdminLogin admin);
        bool UpdateAdmin(AdminLogin admin);
        bool DeleteAdmin(int AdminID);
        bool IsExistUser(string name, string password);
        void Save();

    }
}
