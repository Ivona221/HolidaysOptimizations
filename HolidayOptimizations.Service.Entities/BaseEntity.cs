using System;

namespace HolidayOptimizations.Service.Entities
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }

        public virtual DateTime ModifiedAt { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public BaseEntity()
        {
            ModifiedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }
    }
}
