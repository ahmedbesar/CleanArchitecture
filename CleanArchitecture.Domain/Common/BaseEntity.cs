using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid ID { get; protected set; } = Guid.NewGuid();

        public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DateUpdated { get; private set; }
        public DateTimeOffset? DateDeleted { get; private set; }
        public DateTimeOffset? DateRecovered { get; private set; }

        public Guid CreatorID { get; private set; }
        public Guid? UpdatedID { get; private set; }
        public Guid? DeleterID { get; private set; }
        public Guid? RecoveredID { get; private set; }

        public bool IsDeleted { get; private set; }

        protected BaseEntity() { }

        public void SetCreator(Guid creatorId)
        {
            CreatorID = creatorId;
        }

        public void MarkUpdated(Guid updaterId)
        {
            DateUpdated = DateTimeOffset.UtcNow;
            UpdatedID = updaterId;
        }

        public void Delete(Guid deleterId)
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeleterID = deleterId;
            DateDeleted = DateTimeOffset.UtcNow;
        }

        public void Recover(Guid recoveredById)
        {
            if (!IsDeleted)
                return;

            IsDeleted = false;
            DeleterID = null;
            DateRecovered = DateTimeOffset.UtcNow;
            RecoveredID = recoveredById;
        }
    }
}
