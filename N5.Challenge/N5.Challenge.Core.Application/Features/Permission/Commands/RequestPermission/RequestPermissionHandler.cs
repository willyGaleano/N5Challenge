using Elasticsearch.Net;
using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Enums;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Core.Application.Specifications.Permissions;
using N5.Challenge.Core.Application.Wrappers;
using N5.Challenge.Core.Domain.Entities;
using Nest;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.RequestPermission
{
    public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, Response<int>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRepositoryAsync<Permissions> _repositoryPermissions;
        private readonly IRepositoryAsync<PermissionTypes> _repositoryPermissionTypes;
        public RequestPermissionHandler(IElasticClient elasticClient, IRepositoryAsync<Permissions> repositoryPermissions,
            IRepositoryAsync<PermissionTypes> repositoryPermissionTypes)
        {
            _elasticClient = elasticClient;
            _repositoryPermissions = repositoryPermissions;
            _repositoryPermissionTypes = repositoryPermissionTypes;
        }
        public async Task<Response<int>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _repositoryPermissions.GetByIdAsync(request.PermisoId, cancellationToken);
                if (permission == null)
                {
                    return new Response<int>(-1, false, $"No existe ningún permiso con ID : {request.PermisoId}");
                }
                string description = PermissionTypesEnum.USER.ToString();
                var permissionType = await _repositoryPermissionTypes.GetBySpecAsync(new GetByNameSpecification(description), cancellationToken);

                if(permissionType == null)
                {
                    return new Response<int>(-1, false, $"No existe el tipo de permiso : {description}");
                }
                permission.TipoPermiso = permissionType.Id;

                await _repositoryPermissions.UpdateAsync(permission, cancellationToken);
                
                var respES = await _elasticClient.UpdateAsync<PermissionsDTO, Object>(request.PermisoId, d => d.Doc(
                        new
                        {
                            TipoPermisoId = permissionType.Id,
                            TipoPermiso = permissionType.Descripcion
                        }
                    ).Refresh(Refresh.True), cancellationToken);

                if (!respES.IsValid)
                {
                    return new Response<int>(-1, false, $"No se puso actualizar en ES el permiso con ID : {request.PermisoId}");
                }

                return new Response<int>(request.PermisoId, "Se actualizó correctamente el tipo de permiso");
            }
            catch(Exception ex)
            {
                Log.Error($"RequestPermissionHandler :: ex : {ex.Message}");
                return new Response<int>(-1, false, ex.Message);
            }
        }
    }
}
