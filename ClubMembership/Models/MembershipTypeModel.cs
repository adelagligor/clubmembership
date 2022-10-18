using ClubMembership.Models.DBObjects;

namespace ClubMembership.Models
{
    public class MembershipTypeModel
    {
       

        public Guid IdmembershipType { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int SubscriptionLengthMonths { get; set; }

        
    }
}
