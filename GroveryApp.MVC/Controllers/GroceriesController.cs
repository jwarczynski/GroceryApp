using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers
{
    public class GroceriesController : Controller
    {
        private readonly BLC.BLC _blc;

        public GroceriesController()
        {
            _blc = BLCContainer.Instance;
        }

        // GET: Groceries
        public async Task<IActionResult> Index()
        {
            return View(_blc.GetAllGroceries());
        }

        // GET: Groceries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = _blc.GetAllGroceries()
                .FirstOrDefault(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] IGrocery grocery)
        {
            if (ModelState.IsValid)
            {
                _blc.SaveGrocery(grocery);
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

            var grocery = _blc.GetAllGroceries()
                .FirstOrDefault(m => m.Id == id); ;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] IGrocery grocery)
        {
            if (id != grocery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blc.EditGrocery(grocery);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryExists(grocery.Id))
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

            var grocery = _blc.GetAllGroceries()
                .FirstOrDefault(m => m.Id == id);
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
            var grocery = _blc.GetAllGroceries()
                .FirstOrDefault(m => m.Id == id);
            if (grocery != null)
            {
                _blc.DeleteGrocery(grocery);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GroceryExists(int id)
        {
            return _blc.GetAllGroceries().Any(e => e.Id == id);
        }
    }
}
