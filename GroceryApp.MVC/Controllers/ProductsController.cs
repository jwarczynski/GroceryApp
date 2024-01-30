using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Models;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Filters;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using System.ComponentModel;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsViewModel _productsViewModel;
        private readonly GroceriesViewModel _groceriesViewModel;
        private readonly ApplicationUsersViewModel _applicationUsersViewModel;

        public ProductsController(
            ProductsViewModel productsViewModel, 
            GroceriesViewModel groceriesViewModel,
            ApplicationUsersViewModel applicationUsersViewModel
            )
        {
            _productsViewModel = productsViewModel;
            _groceriesViewModel = groceriesViewModel;
            _applicationUsersViewModel = applicationUsersViewModel;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchName, ICollection<ProductCategory> searchCategories)
        {
            if (!_applicationUsersViewModel.IsLoggedIn())
                return RedirectToAction(nameof(Index), "ApplicationUsers");
            var filter = new Filter()
            {
                Name = searchName,
                Categories = searchCategories
            };
            _productsViewModel.ApplyFiltersCommand.Execute(filter);
            return View(_productsViewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!_applicationUsersViewModel.IsLoggedIn())
                return RedirectToAction(nameof(Index), "ApplicationUsers");
            if (id == null)
            {
                return NotFound();
            }

            var product = _productsViewModel.Products
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (!_applicationUsersViewModel.IsLoggedIn())
                return RedirectToAction(nameof(Index), "ApplicationUsers");
            var modelView = new ProductViewModel()
            {
                Groceries = _groceriesViewModel.Groceries,
            };
            return View(modelView);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Category,Magnesium,Potassium,Sodium,GroceryId")] Product product)
        {
            product.Grocery = _groceriesViewModel.Groceries.Where(g => g.Id == product.GroceryId).SingleOrDefault();
            ModelState.ClearValidationState(nameof(Product));
            if (TryValidateModel(product, nameof(Product)))
            {
                _productsViewModel.AddCommand.Execute(product);
                return RedirectToAction(nameof(Index));
            }
            return View(new ProductViewModel()
            {
                Product = product,
                Groceries = _groceriesViewModel.Groceries,
            });
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_applicationUsersViewModel.IsLoggedIn())
                return RedirectToAction(nameof(Index), "ApplicationUsers");
            if (id == null)
            {
                return NotFound();
            }

            var product = _productsViewModel.Products
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productModelView = new ProductViewModel()
            {
                Product = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Magnesium = product.Magnesium,
                    Potassium = product.Potassium,
                    Sodium = product.Sodium,
                    Grocery = product.Grocery,
                },
                Groceries = _groceriesViewModel.Groceries,
            };
            return View(productModelView);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,Magnesium,Potassium,Sodium,GroceryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            product.Grocery = _groceriesViewModel.Groceries.Where(g => g.Id == product.GroceryId).SingleOrDefault();
            ModelState.ClearValidationState(nameof(Product));
            if (TryValidateModel(product, nameof(Product)))
            {
                try
                {
                    _productsViewModel.UpdateProductCommand.Execute(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists((int)product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_applicationUsersViewModel.IsLoggedIn())
                return RedirectToAction(nameof(Index), "ApplicationUsers");
            if (id == null)
            {
                return NotFound();
            }

            var product = _productsViewModel.Products
                .SingleOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _productsViewModel.Products
                .SingleOrDefault(m => m.Id == id);
            if (product != null)
            {
                _productsViewModel.RemoveCommand.Execute(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productsViewModel.Products.Any(e => e.Id == id);
        }
    }
}
