using Microsoft.AspNetCore.Mvc;
using N5.Challenge.Core.Application.Features.Permission.Commands.CreateDataFake;
using N5.Challenge.Core.Application.Features.Permission.Commands.ModifyPermission;
using N5.Challenge.Core.Application.Features.Permission.Commands.RequestPermission;
using N5.Challenge.Core.Application.Features.Permission.Queries.GetPermissions;
using System.Threading.Tasks;

namespace N5.Challenge.Presentation.WebAPI.Controllers.v1
{
    public class ChallengeController : BaseApiController
    {        
        [HttpPatch("RequestPermission/{permissionId}")]
        public async Task<IActionResult> RequestPermission(int permissionId, [FromBody] RequestPermissionCommand command)
        {
            command.PermisoId = permissionId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("ModifyPermission/{permissionId}")]
        public async Task<IActionResult> ModifyPermission(int permissionId, ModifyPermissionCommand command)
        {
            command.PermisoId = permissionId;
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] GetPermissionsQuery getPermissionsQuery)
        {
            return Ok(await Mediator.Send(getPermissionsQuery));
        }

        [HttpPost("CreateDataFake")]
        public async Task<IActionResult> CreateDataFake([FromBody] CreateDataFakeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

       

    }
}
