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
                        Description = "Mang lại hương vị đậm đà từ cafe, sự ngọt dịu từ sữa.",
                        Price = 29000,
                        Category = "Cà phê",
                        ImageURL = "images/cafe-sua.png"
                    },
                    new Product
                    {
                        Name = "Cà phê đen đá",
                        Description = "Mang lại hương vị đậm đà từ cafe.",
                        Price = 25000,
                        Category = "Cà phê",
                        ImageURL = "images/cafe-den.png"
                    },
                    new Product
                    {
                        Name = "Bạc xỉu",
                        Description = "Mang lại hương vị đậm đà từ vị sữa, thơm mùi cafe.",
                        Price = 25000,
                        Category = "Cà phê",
                        ImageURL = "images/bac_xiu.png"
                    },
                    new Product
                    {
                        Name = "Trà đào cam sả",
                        Description = "Vị ngọt thanh của đào, vị thơm của vỏ cam và hương nồng của sả.",
                        Price = 50000,
                        Category = "Trà",
                        ImageURL = "images/tra-dao-cam-sa.png"
                    },
                    new Product
                    { 
                        Name = "Trà olong đào",
                        Description = "Vị ngọt thanh của đào và hương nồng của trà olong.",
                        Price = 59000,
                        Category = "Trà",
                        ImageURL = "images/tra-olong-dao.png"
                    },
                    new Product
                    {
                        Name = "Trà đào sữa",
                        Description = "Vị ngọt của sữa và đào.",
                        Price = 59000,
                        Category = "Trà",
                        ImageURL = "images/tra-dao-sua.png"
                    },
                    new Product
                    {
                        Name = "Đá xay cafe",
                        Description = "Sự mát lạnh mang hương vị cafe và ngọt bùi của kem cheese.",
                        Price = 59000,
                        Category = "Đá xay",
                        ImageURL = "images/da-xay-cafe.png"
                    },
                    new Product
                    {
                        Name = "Đá xay matcha",
                        Description = "Sự mát lạnh mang hương vị matcha và ngọt bùi của kem cheese.",
                        Price = 59000,
                        Category = "Đá xay",
                        ImageURL = "images/da-xay-matcha.png"
                    },
                    new Product
                    {
                        Name = "Đá xay Chocolate",
                        Description = "Sự mát lạnh mang hương vị sô cô la và ngọt bùi của kem cheese.",
                        Price = 60000,
                        Category = "Đá xay",
                        ImageURL = "images/da-xay-choco.png"
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
