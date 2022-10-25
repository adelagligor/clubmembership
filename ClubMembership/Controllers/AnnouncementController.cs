using ClubMembership.Data;
using ClubMembership.Models;
using ClubMembership.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubMembership.Controllers
{
    public class AnnouncementController : Controller
    {
        private AnnouncementRepository announcementRepository;

        public AnnouncementController(ApplicationDbContext dbContext)
        {
            announcementRepository = new AnnouncementRepository(dbContext);
        }


        // GET: AnnouncementController
        public ActionResult Index()
        {
           var list= announcementRepository.GetAllAnnouncements();
            return View(list);
        }

        // GET: AnnouncementController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = announcementRepository.GetAnnouncementById(id);
            return View("Details", model);
        }

        // GET: AnnouncementController/Create
        public ActionResult Create()
        {
            return View("CreateAnnouncement");
        }

        // POST: AnnouncementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new AnnouncementModel();
                var task= TryUpdateModelAsync(model);
                task.Wait();
                if(task.Result)
                {
                    announcementRepository.InsertAnnouncement(model);

                }
                return View("Index");
            }
            catch
            {
                return RedirectToAction("CreateAnnouncement");
            }
        }

        // GET: AnnouncementController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = announcementRepository.GetAnnouncementById(id);
            return View("Edit", model);
        }

        // POST: AnnouncementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model=new AnnouncementModel();  
                var task= TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    announcementRepository.UpdateAnnouncement(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit",id);
            }
        }

        // GET: AnnouncementController/Delete/5
        [Authorize(Roles ="User, Admin")]
        public ActionResult Delete(Guid id)
        {
            var model = announcementRepository.GetAnnouncementById(id);
            return View("Delete",model);
        }

        // POST: AnnouncementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                announcementRepository.DeleteAnnouncement(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
