using CoffeeStore.Migrations;
using Microsoft.EntityFrameworkCore;
namespace CoffeeStore.Models
{
    public static class CoffeeData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CoffeeDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<CoffeeDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Cà phê sữa đá",
                        Description = "Cà phê Đắk Lắk nguyên chất được pha phin truyền thống kết hợp với sữa đặc tạo nên hương vị đậm đà, hài hòa giữa vị ngọt đầu lưỡi và vị đắng thanh thoát nơi hậu vị.",
                        Price = 29000,
                        Category = "Cà phê",
                        ImageURL = "images/cafe-sua.png"
                    },
                    new Product
                    {
                        Name = "Cà phê đen đá",
                        Description = "Không ngọt ngào như Bạc sỉu hay Cà phê sữa, Cà phê đen mang trong mình phong vị trầm lắng, thi vị hơn. Người ta thường phải ngồi rất lâu mới cảm nhận được hết hương thơm ngào ngạt, phảng phất mùi cacao và cái đắng mượt mà trôi tuột xuống vòm họng.",
                        Price = 25000,
                        Category = "Cà phê",
                        ImageURL = "images/cafe-den.png"
                    },
                    new Product
                    {
                        Name = "Bạc xỉu",
                        Description = "Bạc sỉu chính là \"Ly sữa trắng kèm một chút cà phê\". Thức uống này rất phù hợp những ai vừa muốn trải nghiệm chút vị đắng của cà phê vừa muốn thưởng thức vị ngọt béo ngậy từ sữa.",
                        Price = 25000,
                        Category = "Cà phê",
                        ImageURL = "images/bac_xiu.png"
                    },
                    new Product
                    {
                        Name = "Trà xanh Espresso Marble",
                        Description = "Cho ngày thêm tươi, tỉnh, êm, mượt với Trà Xanh Espresso Marble. Đây là sự mai mối bất ngờ giữa trà xanh Tây Bắc vị mộc và cà phê Arabica Đà Lạt. Muốn ngày thêm chút highlight, nhớ tìm đến sự bất ngờ này bạn nhé!",
                        Price = 49000,
                        Category = "Trà",
                        ImageURL = "images/tra-xanh-espresso-marble.png"
                    },
                    new Product
                    {
                        Name = "Trà đào cam sả",
                        Description = "Vị thanh ngọt của đào, vị chua dịu của Cam Vàng nguyên vỏ, vị chát của trà đen tươi được ủ mới mỗi 4 tiếng, cùng hương thơm nồng đặc trưng của sả chính là điểm sáng làm nên sức hấp dẫn của thức uống này.",
                        Price = 49000,
                        Category = "Trà",
                        ImageURL = "images/tra-dao-cam-sa.png"
                    },
                    new Product
                    {
                        Name = "Oolong tứ quý vải",
                        Description = "Đậm hương trà, thanh mát sắc xuân với Oolong Tứ Quý Vải. Cảm nhận hương hoa đầu mùa, hòa quyện cùng vị vải chín mọng căng tràn sức sống.",
                        Price = 49000,
                        Category = "Trà",
                        ImageURL = "images/Oolong-tu-quy-vai.png"
                    },
                    new Product
                    {
                        Name = "Oolong tứ quý kim quất trân châu",
                        Description = "Đậm hương trà, sảng khoái du xuân cùng Oolong Tứ Quý Kim Quất Trân Châu. Vị nước cốt kim quất tươi chua ngọt, thêm trân châu giòn dai.",
                        Price = 49000,
                        Category = "Trà",
                        ImageURL = "images/Oolong-tu-quy-kim-quat-tran-chau.png"
                    },
                    new Product
                    {
                        Name = "Frosty bánh kem dâu",
                        Description = "Bồng bềnh như một đám mây, Đá Xay Frosty Bánh Kem Dâu vừa ngon mắt vừa chiều vị giác bằng sự ngọt ngào. Bắt đầu bằng cái chạm môi với lớp kem whipping cream, cảm nhận ngay vị beo béo lẫn sốt dâu thơm lừng. Sau đó, hút một ngụm là cuốn khó cưỡng bởi đá xay mát lạnh quyện cùng sốt dâu ngọt dịu. Lưu ý: Khuấy đều phần đá xay trước khi dùng",
                        Price = 59000,
                        Category = "Đá xay",
                        ImageURL = "images/frosty-banh-kem-dau.png"
                    },
                    new Product
                    {
                        Name = "Frosty Choco Chip",
                        Description = "Đá Xay Frosty Choco Chip, thử là đã! Lớp whipping cream bồng bềnh, beo béo lại có thêm bột sô cô la và sô cô la chip trên cùng. Gấp đôi vị ngon với sô cô la thật xay với đá sánh mịn, đậm đà đến tận ngụm cuối cùng.",
                        Price = 59000,
                        Category = "Đá xay",
                        ImageURL = "images/frosty-choco-chip.png"
                    },
                    new Product
                    {
                        Name = "Frosty Caramel Arabica",
                        Description = "Caramel ngọt thơm quyện cùng cà phê Arabica Cầu Đất đượm hương gỗ thông, hạt dẻ và nốt sô cô la đặc trưng tạo nên Đá Xay Frosty Caramel Arabica độc đáo chỉ có tại Nhà. Cực cuốn với lớp whipping cream béo mịn, thêm thạch cà phê giòn ngon bắt miệng.\r\n\r\n",
                        Price = 59000,
                        Category = "Đá xay",
                        ImageURL = "images/frosty-caramel-arabica.png"
                    }
                );

                context.SaveChanges();
            }
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        Phone = "0392055477",
                        FirstName = "Hòa",
                        LastName = "Lê Đỗ Thanh",
                        NgaySinh = new DateTime(2004, 03, 25),
                        Email = "thanhhoa123@gmail.com",
                        GioiTinh = "Nam"
                    },
                    new Customer
                    {
                        Phone = "0392055488",
                        FirstName = "Huy",
                        LastName = "Trần Thanh",
                        NgaySinh = new DateTime(2004, 11, 25),
                        Email = "thanhhuy123@gmail.com",
                        GioiTinh = "Nam"
                    },
                    new Customer
                    {
                        Phone = "0392055499",
                        FirstName = "Bình",
                        LastName = "Phan Thị",
                        NgaySinh = new DateTime(2003, 04, 20),
                        Email = "thibinh123@gmail.com",
                        GioiTinh = "Nữ"
                    }
                );

                context.SaveChanges();
            }
        }

    }
}
