using Cms.BLL.category.viewmodels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.BLL.pages.viewmodels
{
    public class PageView
    {
        public int PageID { get; set; }
        public string Column { get; set; }

        public Language Language { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string PContent { get; set; }

        //[DataType(DataType.Upload)]
        //[FileExtensions(Extensions = ".jpg,.png", ErrorMessage = "图片格式错误")]
        public IFormFile PageImg { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPicturePage { get; set; }
    }
}
