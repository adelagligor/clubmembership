using ClubMembership.Data;
using ClubMembership.Models.DBObjects;
using ClubMembership.Models;

namespace ClubMembership.Repository
{
    public class CodeSnippetRepository
    {

        private readonly ApplicationDbContext _DBContext;
        public CodeSnippetRepository()
        {
            _DBContext = new ApplicationDbContext();
        }

        public CodeSnippetRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private CodeSnippetModel MapDBObjectToModel(CodeSnippet dbobject)
        {
            var model = new CodeSnippetModel();
            if (dbobject != null)
            {
                model.IdcodeSnippet = dbobject.IdcodeSnippet;
                model.Title = dbobject.Title;
                model.ContentCode = dbobject.ContentCode;
                model.Idmember = dbobject.Idmember;
                model.Revision = dbobject.Revision;
                model.IdsnippetPreviousVersion = dbobject.IdsnippetPreviousVersion;
                model.DateTimeAdded = dbobject.DateTimeAdded;
                model.IsPublished = dbobject.IsPublished;
            }
            return model;
        }
        private CodeSnippet MapModelToDBObject(CodeSnippetModel model)
        {
            var dbobject = new CodeSnippet();
            if (model != null)
            {
                dbobject.IdcodeSnippet = model.IdcodeSnippet;
                dbobject.Title = model.Title;
                dbobject.ContentCode = model.ContentCode;
                dbobject.Idmember = model.Idmember;
                dbobject.Revision = model.Revision;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IsPublished = model.IsPublished;

            }
            return dbobject;
        }
        public List<CodeSnippetModel> GetAllCodesnippets()
        {
            var list = new List<CodeSnippetModel>();
            foreach (var dbobject in _DBContext.CodeSnippets
            )
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public CodeSnippetModel GetAnnouncementById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == id));

        }

        public void InsertCodeSnippet(CodeSnippetModel model)
        {
            model.IdcodeSnippet = Guid.NewGuid();
            _DBContext.CodeSnippets.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateCodeSnippet(CodeSnippetModel model)
        {
            var dbobject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == model.IdcodeSnippet);

            if (dbobject != null)
            {
                dbobject.IdcodeSnippet = model.IdcodeSnippet;
                dbobject.Title = model.Title;
                dbobject.ContentCode = model.ContentCode;
                dbobject.Idmember = model.Idmember;
                dbobject.Revision = model.Revision;
                dbobject.DateTimeAdded = model.DateTimeAdded;
                dbobject.IsPublished = model.IsPublished;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteCodeSnippet(CodeSnippetModel model)
        {
            var dbobject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == model.IdcodeSnippet);
            if (dbobject != null)
            {
                _DBContext.CodeSnippets.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}
