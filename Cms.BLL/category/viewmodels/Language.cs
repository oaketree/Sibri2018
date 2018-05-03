using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.BLL.category.viewmodels
{
    public enum Language
    {
        [Display(Name = "中文")]
        Chinese = 0,
        [Display(Name = "English")]
        English = 1
    }
}
