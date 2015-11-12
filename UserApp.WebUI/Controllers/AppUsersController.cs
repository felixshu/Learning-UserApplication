using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using User.Data.Model;

namespace UserApp.WebUI.Controllers
{
	public class AppUsersController : Controller
	{
		public class AppUserRolesController : Controller
		{
			public AppUserRolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
			{
				_userManager = userManager;
				_roleManager = roleManager;
			}

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

			// GET: AppUsers
			public async Task<ActionResult> Index()
			{
				return View(await UserManager.Users.ToListAsync());
			}

			// GET: AppUsers/Details/5
//        public async Task<ActionResult> Details(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            AppUser appUser = await db.AppUsers.FindAsync(id);
//            if (appUser == null)
//            {
//                return HttpNotFound();
//            }
//            return View(appUser);
//        }
//
//        // GET: AppUsers/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

			// POST: AppUsers/Create
			// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
			// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser appUser)
//        {
//            if (ModelState.IsValid)
//            {
//                db.AppUsers.Add(appUser);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//
//            return View(appUser);
//        }

			// GET: AppUsers/Edit/5
			public async Task<ActionResult> Edit(string id)
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				AppUser appUser = await db.AppUsers.FindAsync(id);
				if (appUser == null)
				{
					return HttpNotFound();
				}
				return View(appUser);
			}

			// POST: AppUsers/Edit/5
			// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
			// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

			// GET: AppUsers/Delete/5
//        public async Task<ActionResult> Delete(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            AppUser appUser = await db.AppUsers.FindAsync(id);
//            if (appUser == null)
//            {
//                return HttpNotFound();
//            }
//            return View(appUser);
//        }

			// POST: AppUsers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(string id)
//        {
//            AppUser appUser = await db.AppUsers.FindAsync(id);
//            db.AppUsers.Remove(appUser);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					UserManager.Dispose();
				}
				base.Dispose(disposing);
			}

			#region Private Field

			private ApplicationUserManager _userManager;
			private ApplicationRoleManager _roleManager;

			#endregion
		}
	}