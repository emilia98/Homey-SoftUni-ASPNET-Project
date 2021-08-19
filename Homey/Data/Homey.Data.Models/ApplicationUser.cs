using System;
using System.Collections.Generic;
using Homey.Data.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Homey.Data.Models
{
    public class ApplicationUser: IdentityUser<int>, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Roles = new HashSet<IdentityUserRole<int>>();
            this.Claims = new HashSet<IdentityUserClaim<int>>();
            this.Logins = new HashSet<IdentityUserLogin<int>>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
    }
}