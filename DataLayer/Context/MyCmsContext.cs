using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
   public  class MyCmsContext:DbContext
    {
        //This Class Using for To communicate with the database

        public DbSet<PageGroup> pagegroup { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<PageComment> pagecomment { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }
    }
}
