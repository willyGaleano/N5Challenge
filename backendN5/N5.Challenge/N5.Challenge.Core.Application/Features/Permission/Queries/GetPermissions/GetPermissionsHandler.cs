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
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, PagedResponse<List<PermissionsDTO>>>
    {
        private readonly IElasticClient _elasticClient;
        public GetPermissionsHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<PagedResponse<List<PermissionsDTO>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resultES = await _elasticClient.SearchAsync<PermissionsDTO>(
                               s => s.Query(
                                   q => q.QueryString(
                                       d => d.Query('*' + request.NombreEmpleado + '*')
                                    ))                                    
                                    .From((request.PageNumber - 1) * request.PageSize)
                                    .Size(request.PageSize).Sort(x => x.Ascending(a => a.PermisoId)), cancellationToken);

                if (!resultES.IsValid)
                {
                    Log.Error($"SearchAsync Elastic : {resultES.DebugInformation}");
                    return new PagedResponse<List<PermissionsDTO>>(resultES.DebugInformation);
                }

                var documents = resultES.Documents.ToList();
                var resp = new PagedResponse<List<PermissionsDTO>>(documents, request.PageNumber, request.PageSize, documents.Count, "OK");
                return resp;
            }
            catch(Exception ex)
            {
                Log.Error($"GetPermissionsHandler - Handle :: ex: {ex.Message}");
                return new PagedResponse<List<PermissionsDTO>>(ex.Message);
            }
        }
    }
}
