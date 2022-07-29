using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Core.Application.Wrappers;
using N5.Challenge.Core.Domain.Entities;
using Nest;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.ModifyPermission
{
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, Response<PermissionsDTO>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRepositoryAsync<Permissions> _repositoryPermissions;
        private readonly IRepositoryAsync<PermissionTypes> _repositoryPermissionTypes;

        public ModifyPermissionHandler(IElasticClient elasticClient, IRepositoryAsync<Permissions> repositoryPermissions,
            IRepositoryAsync<PermissionTypes> repositoryPermissionTypes)
        {
            _elasticClient = elasticClient;
            _repositoryPermissions = repositoryPermissions;
            _repositoryPermissionTypes = repositoryPermissionTypes;
        }

        public async Task<Response<PermissionsDTO>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _repositoryPermissions.GetByIdAsync(request.PermisoId, cancellationToken);
                if (permission == null)
                {
                    return new Response<PermissionsDTO>(null, false, $"No existe ningún permiso con ID : {request.PermisoId}");
                }
                var permissionType = await _repositoryPermissionTypes.GetByIdAsync(request.TipoPermisoId, cancellationToken);
                if (permissionType == null)
                {
                    return new Response<PermissionsDTO>(null, false, $"No existe el tipo de permiso con ID : {request.TipoPermisoId}");
                }
                permission.NombreEmpleado = request.NombreEmpleado;
                permission.ApellidoEmpleado = request.ApellidoEmpleado;
                permission.TipoPermiso = request.TipoPermisoId;
                permission.FechaPermiso = request.FechaPermiso;

                await _repositoryPermissions.UpdateAsync(permission, cancellationToken);

                var obj = new PermissionsDTO
                {
                    PermisoId = request.PermisoId,
                    NombreEmpleado = request.NombreEmpleado,
                    ApellidoEmpleado = request.ApellidoEmpleado,
                    TipoPermisoId = request.TipoPermisoId,
                    TipoPermiso = permissionType.Descripcion,
                    FechaPermiso = request.FechaPermiso
                };
                var respES = await _elasticClient.UpdateAsync<PermissionsDTO>(request.PermisoId, d => d.Doc(obj), cancellationToken);

                if (!respES.IsValid)
                {
                    return new Response<PermissionsDTO>(null, false, $"No se puso actualizar en ES el permiso con ID : {request.PermisoId}");
                }

                return new Response<PermissionsDTO>(obj, "Se actualizó correctamente el permiso");
            }
            catch (Exception ex)
            {
                Log.Error($"ModifyPermissionHandler :: ex : {ex.Message}");
                return new Response<PermissionsDTO>(null, false, ex.Message); ;
            }
        }
    }
}
