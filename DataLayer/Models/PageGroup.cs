using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
   public class PageGroup
    {
        [Key]
        public int GroupID { get; set; }


        [Display(Name ="عنوان گروه")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        [MaxLength(150)]
        public string  GruopTitle { get; set; }

        //navigation property
        //Each PageGroup Can Have Multiple Pages
        public virtual List<Page> page { get; set; }

        //Coustractor
        public PageGroup()
        {

        }
    }
}
