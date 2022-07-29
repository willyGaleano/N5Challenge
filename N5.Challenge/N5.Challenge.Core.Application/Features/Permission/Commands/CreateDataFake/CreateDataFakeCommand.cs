using MediatR;
using N5.Challenge.Core.Application.Wrappers;
using System.Collections.Generic;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.CreateDataFake
{
    public class CreateDataFakeCommand : IRequest<Response<List<int>>>
    {
        public int Cant { get; set; }
    }
}
