using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeStore.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerID { get; set; }  // Use Guid for uniqueness

        public string Phone { get; set; } = String.Empty;  // Phone number becomes a regular property

        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; } = String.Empty;
        public string GioiTinh { get; set; } = String.Empty;

        // Thêm thuộc tính navigation để duy trì mối quan hệ một-nhiều với các đơn hàng
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
