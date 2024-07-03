using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace CoffeeStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Please enter an address")]
        public string? Address { get; set; }

        // Thêm trường CustomerID để tạo khóa ngoại
        [Required]
        public Guid CustomerID { get; set; }

        // Thêm thuộc tính navigation
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

    }
}
