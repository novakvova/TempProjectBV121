using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblFilterValues")]
    public class FilterValueEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}
