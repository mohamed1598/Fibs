using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibs.Models
{
    public class ShoppingBag
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int count { get; set; }
        public bool isCheckedOut { get; set; }
        public DateTime? BuyingTime { get; set; }
    }
}
