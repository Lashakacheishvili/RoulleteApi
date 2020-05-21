using System;
using System.Collections.Generic;
using System.Text;

namespace RoulleteApi.Core
{
    // I used to need to create non generic type of base entity, but problem was in Id field, what would be?
    // Didn't find answer on this question and created BaseEntity class without Id field, but it would be incorrect
    // every base entity should have id. So I changed from BaseEntity to TrackedObject, and Generic type BaseEntity is inherited
    // from it.
    // The usage of non generic type BaseEntity was in handling all BaseEntities without knowing Id types.
    // E.x. See RoulleteDbContexts overrided SaveChangesAsync method.

    public abstract class TrackedObject : ITrackedObject
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public interface ITrackedObject
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}
