using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Contract.upload
{
    [Table("Tb_Upload")]
    public class Uploads
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string FileName { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }
    }
}
