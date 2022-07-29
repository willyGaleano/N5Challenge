using MediatR;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Enums;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Core.Application.Wrappers;
using N5.Challenge.Core.Domain.Entities;
using Nest;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.CreateDataFake
{
    public class CreateDataFakeHandler : IRequestHandler<CreateDataFakeCommand, Response<List<int>>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRepositoryAsync<Permissions> _repositoryPermissions;
        private readonly IRepositoryAsync<PermissionTypes> _repositoryPermissionTypes;

        public CreateDataFakeHandler(IRepositoryAsync<Permissions> repositoryPermissions, IElasticClient elasticClient,
            IRepositoryAsync<PermissionTypes> repositoryPermissionTypes)
        {
            _repositoryPermissions = repositoryPermissions;
            _elasticClient = elasticClient;
            _repositoryPermissionTypes = repositoryPermissionTypes;
        }

        public async Task<Response<List<int>>> Handle(CreateDataFakeCommand request, CancellationToken cancellationToken)
        {
            try
            {                                
                int totalPermissions = await _repositoryPermissions.CountAsync(cancellationToken);
                if(totalPermissions >= request.Cant)
                {
                    return new Response<List<int>>(null, false, $"Ya existe data mayor o igual a la cantidad solicitada : {request.Cant}");
                }

                PermissionTypes perType = await GetOrCreatePermissionType(cancellationToken);

                List<Permissions> permissionsFake = GenerateFakeData(request.Cant, perType.Id);

                List<int> listIds = await CreateDataInSQLAndES(permissionsFake, perType, cancellationToken);
                
                return new Response<List<int>>(listIds, "Data fake creada");
            }
            catch (Exception ex)
            {
                Log.Error($"CreateDataFakeHandler Handle :: ex : {ex.Message}");
                return new Response<List<int>>(null, false , ex.Message);
            }
        }

        private async Task<List<int>> CreateDataInSQLAndES(List<Permissions> permissionsFake, PermissionTypes perType, CancellationToken cancellationToken)
        {
            List<int> listIdsTemp = new();
            foreach (var permission in permissionsFake)
            {
                var resp = await _repositoryPermissions.AddAsync(permission, cancellationToken);
                var obj = new PermissionsDTO
                {
                    PermisoId = resp.Id,
                    NombreEmpleado = resp.NombreEmpleado,
                    ApellidoEmpleado = resp.ApellidoEmpleado,
                    TipoPermisoId = resp.TipoPermiso,
                    TipoPermiso = perType.Descripcion,
                    FechaPermiso = resp.FechaPermiso
                };
                var respes = await _elasticClient.IndexAsync(obj, i => i
                    .Id(resp.Id), cancellationToken
                );

                listIdsTemp.Add(resp.Id);
            }

            return listIdsTemp;
        }

        private async Task<PermissionTypes> GetOrCreatePermissionType(CancellationToken cancellationToken)
        {
            var permissionTypeTemp = await _repositoryPermissionTypes.GetByIdAsync<int>(((int)PermissionTypesEnum.DEFAULT), cancellationToken);
            if (permissionTypeTemp == null)
            {
                var perDefault = new PermissionTypes
                {
                    Descripcion = PermissionTypesEnum.DEFAULT.ToString()
                };

                permissionTypeTemp = await _repositoryPermissionTypes.AddAsync(perDefault, cancellationToken);

                var perUser = new PermissionTypes
                {
                    Descripcion = PermissionTypesEnum.USER.ToString()
                };
                await _repositoryPermissionTypes.AddAsync(perUser, cancellationToken);

                var perAdmin = new PermissionTypes
                {
                    Descripcion = PermissionTypesEnum.ADMIN.ToString()
                };
                await _repositoryPermissionTypes.AddAsync(perAdmin, cancellationToken);                              
            }

            return permissionTypeTemp;
        }

        private static List<Permissions> GenerateFakeData(int cant, int perTypeId)
        {
            var listTemp = new List<Permissions>();
            for (var i = 1; i < cant + 1; i++)
            {
                var obj = new Permissions
                {
                    NombreEmpleado = $"Empleado_{(char)i}",
                    ApellidoEmpleado = $"Apellido_{(char)i}",
                    TipoPermiso = perTypeId,
                    FechaPermiso = DateTime.UtcNow
                };
                listTemp.Add(obj);
            }

            return listTemp;
        }
    }
}
