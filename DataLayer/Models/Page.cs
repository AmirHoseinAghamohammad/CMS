using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer
{
   public class Page
    {
        [Key]
        public int PageID { get; set; }


        //foreign Key To PageGroup
        [Display(Name ="گروه صفحه")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        public int GroupID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        [MaxLength(250)]
        public string Title { get; set; }

        [Display(Name ="توضیح مختصر")] 
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")] 
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDiscription { get; set; }

        [Display(Name ="متن")] 
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }


        [Display(Name ="بازدید")]
        public int Visit { get; set; }


        [Display(Name ="تصویر")]
        public string ImageName { get; set; }

        [Display(Name ="اسلایدر")]
        public bool ShowInSlider { get; set; }

        [Display(Name ="تاریخ ایجاد")]
        [DisplayFormat(DataFormatString ="{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "کلمات کلیدی ")]
        public string Tags { get; set; }

        //navigation property
        //Each Page Can Only Be Subset of a PageGroup
        public virtual PageGroup pagegroup { get; set; }

        //Each Page Can Have Multiple PageComment
        public virtual List<PageComment> pagecomment { get; set; }



        //Constractor
        public Page()
        {

        }
    }
}
