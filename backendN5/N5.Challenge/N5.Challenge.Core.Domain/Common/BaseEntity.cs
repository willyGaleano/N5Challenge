namespace N5.Challenge.Core.Domain.Common
{
    public abstract class BaseEntity : AuditableBaseEntity
    {
        public int Id { get; set; }
    }
}
