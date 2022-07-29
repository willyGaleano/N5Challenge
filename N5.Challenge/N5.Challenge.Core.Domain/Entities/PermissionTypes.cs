using N5.Challenge.Core.Domain.Common;
using System.Collections.Generic;

namespace N5.Challenge.Core.Domain.Entities
{
    public class PermissionTypes : BaseEntity
    {        
        public string Descripcion { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
