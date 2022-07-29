using MediatR;
using N5.Challenge.Core.Application.Wrappers;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.RequestPermission
{
    public class RequestPermissionCommand : IRequest<Response<int>>
    {
        public int PermisoId { get; set; }        
    }
}
