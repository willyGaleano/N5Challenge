using N5.Challenge.Core.Domain.Common;
using System;

namespace N5.Challenge.Core.Domain.Entities
{
    public class Permissions : BaseEntity
    {        
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
        public virtual PermissionTypes PermissionTypes { get; set; }
    }
}
