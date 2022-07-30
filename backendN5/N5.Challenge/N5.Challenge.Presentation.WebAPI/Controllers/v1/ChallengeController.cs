using Microsoft.AspNetCore.Mvc;
using N5.Challenge.Core.Application.Features.Permission.Commands.ModifyPermission;
using N5.Challenge.Core.Application.Features.Permission.Commands.RequestPermission;
using N5.Challenge.Core.Application.Features.Permission.Queries.GetPermissions;
using N5.Challenge.Core.Application.Wrappers.Filters;
using System.Threading.Tasks;

namespace N5.Challenge.Presentation.WebAPI.Controllers.v1
{
    public class ChallengeController : BaseApiController
    {        
        [HttpPatch("RequestPermission/{permissionId}")]
        public async Task<IActionResult> RequestPermission(int permissionId)
        {
            RequestPermissionCommand command = new() { PermisoId = permissionId };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("ModifyPermission/{permissionId}")]
        public async Task<IActionResult> ModifyPermission(int permissionId, [FromBody] ModifyPermissionCommand command)
        {
            command.PermisoId = permissionId;
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] GetPermissionsQuery query)
        {
            var validFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            query.PageNumber = validFilter.PageNumber;
            query.PageSize = validFilter.PageSize;
            return Ok(await Mediator.Send(query));
        }
    }
}
