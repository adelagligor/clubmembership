using ClubMembership.Data;
using ClubMembership.Models;
using ClubMembership.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubMembership.Controllers
{
    public class MembershipTypeController : Controller
    {
        private MembershipTypeRepository membershiptypeRepository;

        public MembershipTypeController(ApplicationDbContext dbContext)
        {
            membershiptypeRepository = new MembershipTypeRepository(dbContext);
        }

        // GET: MembershipTypeController
        public ActionResult Index()
        {
            var list = membershiptypeRepository.GetAllMembershipTypes();
            return View(list);
        }

        // GET: MembershipTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MembershipTypeController/Create
        public ActionResult Create()
        {
            return View("CreateMembershipType");
        }

        // POST: MembershipTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MembershipTypeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershiptypeRepository.InsertMembershipType(model);

                }
                return View("Index");
            }
            catch
            {
                return View("CreateMembershipType");
            }
        }

        // GET: MembershipTypeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = membershiptypeRepository.GetMembershipTypeById(id);
            return View("Edit", model);
        }

        // POST: MembershipTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MembershipTypeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    membershiptypeRepository.UpdateMembershipType(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: MembershipTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MembershipTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
