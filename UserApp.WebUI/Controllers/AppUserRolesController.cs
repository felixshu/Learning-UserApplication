using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Service.DataContext;
using Microsoft.AspNet.Identity.Owin;
using User.Data.Model;
using ViewModel.Model;

namespace UserApp.WebUI.Controllers
{
	[Authorize(Roles = "GeneralManager")]
	public class AppUserRolesController : Controller
	{
		#region Private Field

		private ApplicationUserManager _userManager;
		private ApplicationRoleManager _roleManager;

		#endregion

		public ApplicationUserManager UserManager
		{
			get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
			set { _userManager = value; }
		}

		public ApplicationRoleManager RoleManager
		{
			get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
			set { _roleManager = value; }
		}

		public AppUserRolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// GET: AppUserRoles
		public async Task<ActionResult> Index()
		{
			return View(await RoleManager.Roles.ToListAsync());
		}

		// GET: AppUserRoles/Details/5
		public async Task<ActionResult> Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AppUserRole appUserRole = await RoleManager.FindByIdAsync(id);
			if (appUserRole == null)
			{
				return HttpNotFound();
			}
			return View(appUserRole);
		}

		//GET: AppUserRoles/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AppUserRoles/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is null.</exception>
		/// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Name")] AppUserRoleVm appUserRoleVm)
		{
			if (ModelState.IsValid)
			{
				var appUserRole = new AppUserRole() {Name = appUserRoleVm.Name};
				var roleResult = await RoleManager.CreateAsync(appUserRole);
				if (!roleResult.Succeeded)
				{
					ModelState.AddModelError("", roleResult.Errors.First());
					return View();
				}
				return RedirectToAction("Index");
			}

			return View(appUserRoleVm);
		}

		// GET: AppUserRoles/Edit/5
		public async Task<ActionResult> Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AppUserRole appUserRole = await RoleManager.FindByIdAsync(id);
			if (appUserRole == null)
			{
				return HttpNotFound();
			}
			return View(appUserRole);
		}

		// POST: AppUserRoles/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] AppUserRole appUserRole)
		{
			if (ModelState.IsValid)
			{
				var retrievedAppRole = await RoleManager.FindByIdAsync(appUserRole.Id);
				var origianlName = retrievedAppRole.Name;

				if (origianlName == "General Manager" && appUserRole.Name != "General Manager")
				{
					ModelState.AddModelError("","You cannot change the name of the General Manager role");
					return View(appUserRole);
				}

				if (origianlName != "General Manger" && appUserRole.Name == "General Manager")
				{
					ModelState.AddModelError("","You cannot change the name of role to General Manager");
					return View(appUserRole);
				}

				retrievedAppRole.Name = appUserRole.Name;
				await RoleManager.UpdateAsync(retrievedAppRole);

				return RedirectToAction("Index");
			}
			return View(appUserRole);
		}

		// GET: AppUserRoles/Delete/5
		public async Task<ActionResult> Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var appUserRole = await RoleManager.FindByIdAsync(id);
			if (appUserRole == null)
			{
				return HttpNotFound();
			}
			return View(appUserRole);
		}

		// POST: AppUserRoles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id)
		{
			var appUserRole = await RoleManager.FindByIdAsync(id);

			if (appUserRole.Name == "General Manager")
			{
				ModelState.AddModelError("","You cannot delete General Manager Role");
				return View(appUserRole);
			}

			await RoleManager.DeleteAsync(appUserRole);
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				RoleManager.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}