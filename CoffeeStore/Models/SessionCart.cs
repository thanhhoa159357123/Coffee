using CoffeeStore.Infrastructure;
using System.Text.Json.Serialization;

namespace CoffeeStore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext?.Session; // Lấy session từ HttpContext
            SessionCart cart = session?.GetJson<SessionCart>("Cart") 
                ?? new SessionCart();
            cart.Session = session;
            return cart; 
        }

        [JsonIgnore]
        public ISession? Session { get; set; } 

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity); 
            Session?.SetJson("Cart", this); 
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product); // Gọi phương thức RemoveLine của lớp cơ sở (Cart)
            Session?.SetJson("Cart", this); // Cập nhật giỏ hàng trong session
        }

        // Ghi đè phương thức Clear để xóa toàn bộ giỏ hàng và session liên quan
        public override void Clear()
        {
            base.Clear(); // Gọi phương thức Clear của lớp cơ sở (Cart)
            Session?.Remove("Cart"); // Xóa giỏ hàng khỏi session
        }

        // Ghi đè phương thức UpdateQuantity để cập nhật số lượng của một mục trong giỏ hàng và lưu vào session
        public override void UpdateQuantity(Product product, int quantity)
        {
            base.UpdateQuantity(product, quantity); // Gọi phương thức UpdateQuantity của lớp cơ sở (Cart)
            Session?.SetJson("Cart", this); // Cập nhật giỏ hàng trong session
        }
    }
}
