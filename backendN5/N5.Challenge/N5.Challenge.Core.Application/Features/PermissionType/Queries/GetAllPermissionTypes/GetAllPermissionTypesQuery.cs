using MediatR;
using N5.Challenge.Core.Application.DTOs.PermissionTypes;
using N5.Challenge.Core.Application.Wrappers;
using System.Collections.Generic;

namespace N5.Challenge.Core.Application.Features.PermissionType.Queries.GetAllPermissionTypes
{
    public class GetAllPermissionTypesQuery : IRequest<Response<List<PermissionTypesDTO>>>
    {
    }
}
