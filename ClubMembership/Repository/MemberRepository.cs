using ClubMembership.Data;
using ClubMembership.Models.DBObjects;
using ClubMembership.Models;

namespace ClubMembership.Repository
{
    public class MemberRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public MemberRepository()
        {
            _DBContext = new ApplicationDbContext();
        }

        public MemberRepository(ApplicationDbContext dbContext)   
        {
            _DBContext = dbContext;
        }

        private MemberModel MapDBObjectToModel(Member dbobject)
        {
            var model = new MemberModel();
            if (dbobject != null)
            {
                model.Idmember = dbobject.Idmember;
                model.Name = dbobject.Name;
                model.Title = dbobject.Title;
                model.Position = dbobject.Position;
                model.Description = dbobject.Description;
                model.Resume = dbobject.Resume;
            }
            return model;
        }
        private Member MapModelToDBObject(MemberModel model)
        {
            var dbobject = new Member();
            if (model != null)
            {
                dbobject.Idmember = model.Idmember;
                dbobject.Name = model.Name;
                dbobject.Title = model.Title;
                dbobject.Position = model.Position;
                dbobject.Description = model.Description;
                dbobject.Resume = model.Resume;

            }
            return dbobject;
        }
        public List<MemberModel> GetAllMembers()
        {
            var list = new List<MemberModel>();
            foreach (var dbobject in _DBContext.Members)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public MemberModel GetMemberById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Members.FirstOrDefault(x => x.Idmember == id));

        }

        public void InsertMember(MemberModel model)
        {
            model.Idmember = Guid.NewGuid();
            _DBContext.Members.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateMember(MemberModel model)
        {
            var dbobject = _DBContext.Members.FirstOrDefault(x => x.Idmember == model.Idmember);
            
            if (dbobject != null)
            {
                dbobject.Idmember = model.Idmember;
                dbobject.Name = model.Name;
                dbobject.Title = model.Title;
                dbobject.Position = model.Position;
                dbobject.Description = model.Description;
                dbobject.Resume = model.Resume;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteMember(MemberModel model)
        {
            var dbobject = _DBContext.Members.FirstOrDefault(x => x.Idmember == model.Idmember);
            if (dbobject != null)
            {
                _DBContext.Members.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }
    }
}
