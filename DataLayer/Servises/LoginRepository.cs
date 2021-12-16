using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginRepository : ILoginRepository
    {

        private MyCmsContext DB;
        public LoginRepository(MyCmsContext Context)
        {
            DB = Context;
        }

        public IEnumerable<AdminLogin> GetAll()
        {
            return DB.AdminLogin;
        }
        public AdminLogin GetAdminByID(int AdminID)
        {
            return DB.AdminLogin.Find(AdminID);
        }
        public bool InsertAdmin(AdminLogin admin)
        {
            try
            {
                DB.AdminLogin.Add(admin);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateAdmin(AdminLogin admin)
        {
            try
            {
                DB.Entry(admin).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteAdmin(int AdminID)
        {
            try
            {
                var Find = GetAdminByID(AdminID);
                DB.AdminLogin.Remove(Find);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsExistUser(string name, string password)
        {
            return DB.AdminLogin.Any(u => u.UserName == name && u.Password == password);
        }

        public void Save()
        {
            DB.SaveChanges();
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
