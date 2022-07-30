using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Wrappers;
using System.Collections.Generic;

namespace N5.Challenge.Core.Application.Features.Permission.Queries.GetPermissions
{
    public class GetPermissionsQuery : IRequest<Response<List<PermissionsDTO>>>
    {
        public string NombreEmpleado { get; set; }
    }
}