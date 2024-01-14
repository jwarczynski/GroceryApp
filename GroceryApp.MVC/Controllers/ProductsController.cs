using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.ViewModels;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDAO _blc;

        public ProductsController(IDAO blc)
        {
            _blc = blc;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_blc.GetAllProducts().ToList());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _blc.GetAllProducts()
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var modelView = new ProductModelView()
            {
                Groceries = _blc.GetAllGroceries(),
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
            product.Grocery = _blc.GetAllGroceries().Where(g=>g.Id == product.GroceryId).FirstOrDefault();
            ModelState.ClearValidationState(nameof(Product));
            if (TryValidateModel(product, nameof(Product)))
            {
                _blc.SaveProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(new ProductModelView() { 
                Product=product,
                Groceries = _blc.GetAllGroceries(),
            });
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _blc.GetAllProducts()
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productModelView = new ProductModelView()
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
                Groceries = _blc.GetAllGroceries(),
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
            product.Grocery = _blc.GetAllGroceries().Where(g => g.Id == product.GroceryId).FirstOrDefault();
            ModelState.ClearValidationState(nameof(Product));
            if (TryValidateModel(product, nameof(Product)))
            {
                try
                {
                    _blc.EditProduct(product);
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
            if (id == null)
            {
                return NotFound();
            }

            var product = _blc.GetAllProducts()
                .FirstOrDefault(m => m.Id == id);
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
            var product = _blc.GetAllProducts()
                .FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                _blc.DeleteProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _blc.GetAllProducts().Any(e => e.Id == id);
        }
    }
}
