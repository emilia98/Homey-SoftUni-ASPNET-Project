using System;
using Homey.Data.Common.Models;

namespace Homey.Data.Models
{
    public class UserProfile : IDeletableEntity, IAuditInfo
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? PhotoId { get; set; }

        public virtual UserPhoto UserPhoto { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
