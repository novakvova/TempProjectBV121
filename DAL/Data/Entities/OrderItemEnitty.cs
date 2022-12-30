﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblOrderItems")]
    public class OrderItemEnitty : BaseEntity<int>
    {
        public decimal PriceBuy { get; set; }
        public short Count { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual OrderEntity Order { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public  virtual ProductEntity Product { get; set; }
    }
}
