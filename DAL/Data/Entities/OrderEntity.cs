using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblOrders")]
    public class OrderEntity : BaseEntity<int>
    {
        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }
        public virtual OrderStatusEnitty OrderStatus { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ICollection<OrderItemEnitty> OrderItems { get; set; }
    }
}
