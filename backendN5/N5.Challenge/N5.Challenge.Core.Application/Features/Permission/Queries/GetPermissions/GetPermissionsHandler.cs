using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Wrappers;
using Nest;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Core.Application.Features.Permission.Queries.GetPermissions
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, Response<List<PermissionsDTO>>>
    {
        private readonly IElasticClient _elasticClient;
        public GetPermissionsHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<Response<List<PermissionsDTO>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resultES = await _elasticClient.SearchAsync<PermissionsDTO>(
                               s => s.Query(
                                   q => q.QueryString(
                                       d => d.Query('*' + request.NombreEmpleado + '*')
                                    )).Size(5000));

                var resp = new Response<List<PermissionsDTO>>(resultES.Documents.OrderBy(x => x.PermisoId).ToList());
                return resp;
            }
            catch(Exception ex)
            {
                Log.Error($"GetPermissionsHandler - Handle :: ex: {ex.Message}");
                return new Response<List<PermissionsDTO>>(null, ex.Message);
            }
        }
    }
}
