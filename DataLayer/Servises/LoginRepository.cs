using System;
using System.Collections.Generic;
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
        public bool IsExistUser(string name, string password)
        {
            return DB.AdminLogin.Any(u => u.UserName == name && u.Password == password);
        }
    }
}
