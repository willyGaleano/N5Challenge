using System;

namespace N5.Challenge.Core.Application.DTOs.Permissions
{
    public class PermissionsDTO
    {
        public int PermisoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public string TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }        
    }
}
