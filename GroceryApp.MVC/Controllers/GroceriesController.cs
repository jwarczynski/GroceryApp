using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Models;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers
{
    public class GroceriesController : Controller
    {
        private readonly GroceriesViewModel _groceriesViewModel;

        public GroceriesController(GroceriesViewModel groceriesViewModel)
        {
            _groceriesViewModel = groceriesViewModel;
        }

        // GET: Groceries
        public async Task<IActionResult> Index(string searchString)
        {
            _groceriesViewModel.ApplyFiltersCommand.Execute(searchString);
            return View(_groceriesViewModel);
        }

        // GET: Groceries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = _groceriesViewModel.Groceries
                .SingleOrDefault(m => m.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }

            return View(grocery);
        }

        // GET: Groceries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groceries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Grocery grocery)
        {
            if (ModelState.IsValid)
            {
                _groceriesViewModel.AddCommand.Execute(grocery);
                return RedirectToAction(nameof(Index));
            }
            return View(grocery);
        }

        // GET: Groceries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = _groceriesViewModel.Groceries
                .SingleOrDefault(m => m.Id == id); ;
            if (grocery == null)
            {
                return NotFound();
            }
            return View(grocery);
        }

        // POST: Groceries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Grocery grocery)
        {
            if (id != grocery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _groceriesViewModel.UpdateGroceryCommand.Execute(grocery);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryExists((int)grocery.Id))
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
            return View(grocery);
        }

        // GET: Groceries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = _groceriesViewModel.Groceries
                .SingleOrDefault(m => m.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }

            return View(grocery);
        }

        // POST: Groceries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grocery = _groceriesViewModel.Groceries
                .SingleOrDefault(m => m.Id == id);
            if (grocery != null)
            {
                _groceriesViewModel.RemoveCommand.Execute(grocery);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GroceryExists(int id)
        {
            return _groceriesViewModel.Groceries.Any(e => e.Id == id);
        }
    }
}
