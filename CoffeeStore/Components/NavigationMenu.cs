using CoffeeStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeStore.Components
{
    public class NavigationMenu : ViewComponent
    {
        private ICoffeeRepository repository;
        public NavigationMenu(ICoffeeRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
        }



    }
}
