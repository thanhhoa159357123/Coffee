using System.ComponentModel.DataAnnotations;

namespace CoffeeStore.Models.ViewModels
{
    public class OrderViewModel
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string? Address { get; set; }
    }
}
