using System;

namespace RoulleteApi.Core
{
    public abstract class BaseEntity<K> : TrackedObject, IBaseEntity<K> where K : IComparable
    {
        public K Id { get; set; }

        public BaseEntity()
        {
            // Create and Update date to be equal, otherwise there will be difference in ticks
            var dateTimeNow = DateTime.UtcNow;

            CreatedAt = dateTimeNow;
            UpdatedAt = dateTimeNow;
        }
    }

    public interface IBaseEntity<K> : ITrackedObject where K : IComparable
    {
        K Id { get; set; }
    }
}
