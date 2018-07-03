using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Contract.category
{
    [Table("Tb_Category")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(100)]
        public string CategoryNameEN { get; set; }

        [StringLength(50)]
        public string Href { get; set; }

        //[StringLength(50)]
        //public string HrefEn { get; set; }

        public int? ParentID { get; set; }

        public int? SortID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        public bool? IsHref { get; set; }

        //public List<News> News { get; set; }



    }
}
