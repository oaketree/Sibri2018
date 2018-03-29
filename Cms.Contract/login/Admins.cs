﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Contract.login
{
    [Table("Tb_Admin")]
    public class Admins
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage ="请输入用户名")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(50)]
        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }



    }
}
