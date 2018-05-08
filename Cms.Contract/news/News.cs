using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Contract.news
{
    [Table("Tb_News")]
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        [StringLength(100)]
        public string Title { get; set; }

        public string NewsDetail { get; set; }
        [StringLength(50)]
        public string SubTitle { get; set; }

        //[ForeignKey("Category")]
        public int ColumnID { get; set; }

        public int Hit { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        public int? Language { get; set; }
        [StringLength(50)]
        public string NewsImageName { get; set; }

        //public bool IsPictureNews { get; set; }

        //public virtual Category Category { get; set; }

        public string ShortRegDate {
            get{
                return RegDate.Value.ToString("D");
            }
        }


    }
}
