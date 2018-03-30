using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.BLL.login.viewmodels
{
    public class AdminView
    {
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(50)]
        public string Password { get; set; }

        public bool isPersistent { get; set; }
    }
}
