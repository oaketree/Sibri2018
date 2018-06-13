using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.Contract.pages
{
    [Table("Tb_Page")]
    public class Pages
    {
        [Key]
        public int PageID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public int ColumnID { get; set; }
        public int Hit { get; set; }
        public int Language { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        [StringLength(50)]
        public string PageImageName { get; set; }


        public string PContent { get; set; }



        public string ShortRegDate
        {
            get
            {
                return RegDate.Value.ToString("D");
            }
        }
    }
}
