using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Models;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers
{
	public class ApplicationUsersController : Controller
	{
		private readonly ApplicationUsersViewModel _applicationUsersViewModel;

		public ApplicationUsersController(ApplicationUsersViewModel applicationUsersViewModel)
		{
			_applicationUsersViewModel = applicationUsersViewModel;
		}

		// GET: ApplicationUsers
		public async Task<IActionResult> Index()
		{
			return View(_applicationUsersViewModel.User);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index([Bind("Name,Password")] ApplicationUser applicationUser)
		{
			if (ModelState.IsValid)
			{
				var isLoggedIn = _applicationUsersViewModel.LogIn(applicationUser.Name, applicationUser.Password);
				if (isLoggedIn) RedirectToAction(nameof(Index), "HomeController");
			}
			return View(applicationUser);
		}

		// GET: ApplicationUsers/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ApplicationUsers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Password")] ApplicationUser applicationUser)
		{
			if (ModelState.IsValid)
			{
				_applicationUsersViewModel.RegisterUser(applicationUser.Name, applicationUser.Password);
			}
			return View(applicationUser);
		}

		// GET: ApplicationUsers/Edit
		public async Task<IActionResult> Edit()
		{
			if (!_applicationUsersViewModel.IsLoggedIn())
			{
				return NotFound();
			}
			return View(_applicationUsersViewModel.User);
		}

		// POST: ApplicationUsers/Edit
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("Name,Password")] ApplicationUser applicationUser)
		{
			if (applicationUser.Password == _applicationUsersViewModel.User?.Password) return RedirectToAction(nameof(Index));
			if (ModelState.IsValid)
			{
				_applicationUsersViewModel.ChangePassword(applicationUser);
				return RedirectToAction(nameof(Index));
			}
			return View(applicationUser);
		}

		// GET: ApplicationUsers/Delete/
		public async Task<IActionResult> Delete()
		{
			if (!_applicationUsersViewModel.IsLoggedIn())
			{
				return NotFound();
			}

			return View(_applicationUsersViewModel.User);
		}

		// POST: ApplicationUsers/Delete/
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed()
		{
			if (_applicationUsersViewModel.IsLoggedIn())
			{
				_applicationUsersViewModel.DeleteUser();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
