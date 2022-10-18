using ClubMembership.Data;
using ClubMembership.Models.DBObjects;
using ClubMembership.Models;

namespace ClubMembership.Repository
{
    public class MembershipRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public MembershipRepository()
        {
            _DBContext = new ApplicationDbContext();
        }

        public MembershipRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private MembershipModel MapDBObjectToModel(Membership dbobject)
        {
            var model = new MembershipModel();
            if (dbobject != null)
            {
                model.Idmembership = dbobject.Idmembership;
                model.Idmember = dbobject.Idmember;
                model.IdmembershipType = dbobject.IdmembershipType;
                model.StartDate = dbobject.StartDate;
                model.EndDate = dbobject.EndDate;
                model.Level = dbobject.Level;
            }
            return model;
        }
        private Membership MapModelToDBObject(MembershipModel model)
        {
            var dbobject = new Membership();
            if (model != null)
            {
                dbobject.Idmembership = model.Idmembership;
                dbobject.Idmember = model.Idmember;
                dbobject.IdmembershipType = model.IdmembershipType;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.Level = model.Level;

            }
            return dbobject;
        }
        public List<MembershipModel> GetAllMemberships()
        {
            var list = new List<MembershipModel>();
            foreach (var dbobject in _DBContext.Memberships)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public MembershipModel GetMembershipById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Memberships.FirstOrDefault(x => x.Idmembership == id));

        }

        public void InsertMembership(MembershipModel model)
        {
            model.Idmembership = Guid.NewGuid();
            _DBContext.Memberships.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateMemberships(MembershipModel model)
        {
            var dbobject = _DBContext.Memberships.FirstOrDefault(x => x.Idmembership == model.Idmembership);

            if (dbobject != null)
            {
                dbobject.Idmembership = model.Idmembership;
                dbobject.Idmember = model.Idmember;
                dbobject.IdmembershipType = model.IdmembershipType;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.Level = model.Level;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteMemberhip(MembershipModel model)
        {
            var dbobject = _DBContext.Memberships.FirstOrDefault(x => x.Idmembership == model.Idmembership);
            if (dbobject != null)
            {
                _DBContext.Memberships.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}
