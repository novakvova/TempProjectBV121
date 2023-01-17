using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    /// <summary>
    /// Назви фільтрів
    /// </summary>
    [Table("tblFilterNames")]
    public class FilterNameEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}
