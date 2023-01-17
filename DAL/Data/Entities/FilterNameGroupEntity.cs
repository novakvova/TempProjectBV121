using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblFilterNameGroups")]
    public class FilterNameGroupEntity
    {
        [ForeignKey("FilterName")]
        public int FilterNameId { get; set; }
        [ForeignKey("FilterValue")]
        public int FilterValueId { get; set; }

        public virtual FilterNameEntity FilterName { get; set; }
        public virtual FilterValueEntity FilterValue { get; set; }
    }
}
