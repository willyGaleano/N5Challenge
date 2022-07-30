using Microsoft.AspNetCore.Mvc;
using N5.Challenge.Core.Application.Features.Permission.Commands.CreateDataFake;
using N5.Challenge.Core.Application.Features.PermissionType.Queries.GetAllPermissionTypes;
using System.Threading.Tasks;

namespace N5.Challenge.Presentation.WebAPI.Controllers.v1
{
    public class ExtraController : BaseApiController
    {

        [HttpPost("CreateDataFake")]
        public async Task<IActionResult> CreateDataFake([FromBody] CreateDataFakeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("GetAllPermissionTypes")]
        public async Task<IActionResult> GetAllPermissionTypes()
        {
            return Ok(await Mediator.Send(new GetAllPermissionTypesQuery()));
        }
    }
}
