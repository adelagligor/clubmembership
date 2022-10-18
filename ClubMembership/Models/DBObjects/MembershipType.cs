﻿using System;
using System.Collections.Generic;

namespace ClubMembership.Models.DBObjects
{
    public partial class MembershipType
    {
        public MembershipType()
        {
            Memberships = new HashSet<Membership>();
        }

        public Guid IdmembershipType { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int SubscriptionLengthMonths { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
