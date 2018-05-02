using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.BLL.news.viewmodels
{
    public class NewsView
    {
        public int NewsID { get; set; }
        public string Column { get; set; }

        public Language Language { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string SubTitle { get; set; }

        [Required]
        public string Content { get; set; }

        //[DataType(DataType.Upload)]
        //[FileExtensions(Extensions = ".jpg,.png", ErrorMessage = "图片格式错误")]
        public IFormFile NewsImg { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPictureNews { get; set; }

    }
    public enum Language
    {
        [Display(Name = "中文")]
        Chinese = 0,
        [Display(Name = "English")]
        English = 1
    }
}
