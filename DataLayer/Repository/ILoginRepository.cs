﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface ILoginRepository
    {
        bool IsExistUser(string name, string password);

    }
}
