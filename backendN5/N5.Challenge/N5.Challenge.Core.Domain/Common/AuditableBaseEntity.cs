using System;

namespace N5.Challenge.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {        
        public DateTime Created { get; set; }     
        public DateTime? LastModified { get; set; }
    }
}
