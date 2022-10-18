using ClubMembership.Data;
using ClubMembership.Models;
using ClubMembership.Models.DBObjects;

namespace ClubMembership.Repository
{
    public class AnnouncementRepository
    {
        private readonly ApplicationDbContext _DBContext;   //in Data am ApplicationDbContext care a fost generat la Scaffold(crrez o instanta 
        //din clasa respectiva; folosim _DBContext sa comunicam cu baza de date
        public AnnouncementRepository()  //in constuctor aloc o valoare pentru variabila _DBContext
        {
            _DBContext = new ApplicationDbContext();
        }

        public AnnouncementRepository(ApplicationDbContext dbContext)   // primeste dbContext din afara lui in momentul in care este contruit obiectul
        {
            _DBContext = dbContext;
        }

        private AnnouncementModel MapDBObjectToModel(Announcement dbobject)
        {
            var model = new AnnouncementModel();
            if (dbobject != null)
            {
                model.Idannouncement = dbobject.Idannouncement;
                model.ValidFrom = dbobject.ValidFrom;
                model.ValidTo = dbobject.ValidTo;
                model.Title = dbobject.Title;
                model.Text = dbobject.Text;
                model.EventDateTime = dbobject.EventDateTime;
                model.Tags = dbobject.Tags;

            }
            return model;
        }
        private Announcement MapModelToDBObject(AnnouncementModel model)
        {
            var dbobject = new Announcement();
            if (model != null)
            {
                dbobject.Idannouncement = model.Idannouncement;
                dbobject.ValidFrom = model.ValidFrom;
                dbobject.ValidTo = model.ValidTo;
                dbobject.Title = model.Title;
                dbobject.Text = model.Text;
                dbobject.EventDateTime = model.EventDateTime;
                dbobject.Tags = model.Tags;

            }
            return dbobject;
        }
        public List<AnnouncementModel> GetAllAnnouncements()
        {
            var list = new List<AnnouncementModel>();
            foreach (var dbobject in _DBContext.Announcements)   //citim din _DBContext lista de Announcements; itereaza peste toate dbobjects care sunt in Announcements
            {
                list.Add(MapDBObjectToModel(dbobject)); //avem dbobjects, pe care le convertim in Modele iar apoi le punem in lista
            }
            return list;
        }
        public AnnouncementModel GetAnnouncementById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Announcements.FirstOrDefault(x => x.Idannouncement == id));
            //pe colectia Announcements aplic LambdaExpression FirstorDefault(nu pun datatype pentru ca o sa-l deduca implicit) si expresia
            //Lambda expressionul cauta in toata lista de Announcements pana cand gaseste dbobject care are proprietarea de Idannouncement egala cu id-ul pe care l-am dat ca parametru
            //returneaza dbobjectul respectiv sau default care e nul si-l da la MapDBObjectToModel care il mapeaza si il converteste in model si merge la return si astfel functia returneaza modelul
        }

        public void InsertAnnouncement(AnnouncementModel model) //de ce vine din View?
        {
            model.Idannouncement = Guid.NewGuid();// trebuie sa-i dam un ID nou, vine de pe view si nu are id
            _DBContext.Announcements.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateAnnouncement(AnnouncementModel model)
        {
            var dbobject = _DBContext.Announcements.FirstOrDefault(x => x.Idannouncement == model.Idannouncement);
            //pe _DBContext pe colectia de Announcements folosesc Lambda 
            if (dbobject != null)
            {
                dbobject.Idannouncement = model.Idannouncement;
                dbobject.ValidFrom = model.ValidFrom;
                dbobject.ValidTo = model.ValidTo;
                dbobject.Title = model.Title;
                dbobject.Text = model.Text;
                dbobject.EventDateTime = model.EventDateTime;
                dbobject.Tags = model.Tags;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteAnnouncement(AnnouncementModel model)
        {
            var dbobject = _DBContext.Announcements.FirstOrDefault(x => x.Idannouncement == model.Idannouncement);
            if(dbobject != null)
            {
                _DBContext.Announcements.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }


    }
}
