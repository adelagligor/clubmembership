using ClubMembership.Data;
using ClubMembership.Models;
using ClubMembership.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ClubMembership.Controllers
{
    public class CodeSnippetController : Controller
    {
        private CodeSnippetRepository codesnippetRepository;
        private MemberRepository memberRepository;
        public CodeSnippetController(ApplicationDbContext dbContext)
        {
           memberRepository = new MemberRepository(dbContext);  
           codesnippetRepository = new CodeSnippetRepository(dbContext);
        }
         //GET: CodeSnippetController
        public ActionResult Index()
        {
            var list = codesnippetRepository.GetAllCodesnippets();
            return View(list);
        }

        // GET: CodeSnippetController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: CodeSnippetController/Create
        public ActionResult Create()
        {
            var members=memberRepository.GetAllMembers();
            var memberList = members.Select(x => new SelectListItem(x.Name, x.Idmember.ToString()));
            ViewBag.MemberList = memberList;
            return View("CreateCodeSnippet");
        }

        // POST: CodeSnippetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    codesnippetRepository.InsertCodeSnippet(model);

                }
                return View("Index");
            }
            catch
            {
                return View("CreateCodeSnippet");
            }
        }

        // GET: CodeSnippetController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = codesnippetRepository.GetCodeSnippetById(id);
            return View("Edit", model);
        }

        // POST: CodeSnippetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new CodeSnippetModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    codesnippetRepository.UpdateCodeSnippet(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: CodeSnippetController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = codesnippetRepository.GetCodeSnippetById(id);
            return View("Delete", model);
        }

        // POST: CodeSnippetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                codesnippetRepository.DeleteCodeSnippet(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
