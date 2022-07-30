using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Wrappers;
using System;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.ModifyPermission
{
    public class ModifyPermissionCommand : IRequest<Response<PermissionsDTO>>
    {
        public int PermisoId { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }        
        public DateTime FechaPermiso { get; set; }
    }
}
