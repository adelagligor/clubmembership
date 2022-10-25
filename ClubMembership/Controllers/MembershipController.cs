using ClubMembership.Data;
using ClubMembership.Models;
using ClubMembership.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubMembership.Controllers
{
    public class MembershipController : Controller
    {
        private MembershipRepository membershipRepository;

        public MembershipController(ApplicationDbContext dbContext)
        {
            membershipRepository = new MembershipRepository(dbContext);
        }
        // GET: MembershipController
        public ActionResult Index()
        {
            var list = membershipRepository.GetAllMemberships();
            return View(list);
        }

        // GET: MembershipController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MembershipController/Create
        public ActionResult Create()
        {
            return View("CreateMembership");
        }

        // POST: MembershipController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MembershipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershipRepository.InsertMembership(model);

                }
                return View("Index");
            }
            catch
            {
                return View("CreateMembership");
            }
        }

        // GET: MembershipController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = membershipRepository.GetMembershipById(id);
            return View("Edit", model);
        }

        // POST: MembershipController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MembershipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershipRepository.UpdateMembership(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: MembershipController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = membershipRepository.GetMembershipById(id);
            return View("Delete", model);
        }

        // POST: MembershipController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                membershipRepository.DeleteMembership(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
