using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Core.Domain.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Challenge.Infraestructure.Persistence.Seeds
{
    public static class DefaultPermissions
    {
        public static async Task SeedAsync(IRepositoryAsync<Permissions> myRepository, IElasticClient elasticClient)
        {
            List<Permissions> permissionsDef = GenerateFakeData(10);
            foreach(var permission in permissionsDef)
            {
                var resp = await myRepository.AddAsync(permission);
                var obj = new PermissionsDTO
                {
                    PermisoId = resp.Id,
                    NombreEmpleado = resp.NombreEmpleado,
                    ApellidoEmpleado = resp.ApellidoEmpleado,
                    TipoPermisoId = resp.TipoPermiso,
                    TipoPermiso = string.Empty,
                    FechaPermiso = resp.FechaPermiso
                };
                await elasticClient.UpdateAsync<PermissionsDTO>(resp.Id, d => d.Doc(obj));
            }
        }

        private static List<Permissions> GenerateFakeData(int cant)
        {
            var listTemp = new List<Permissions>();
            for(var i = 1; i < cant + 1; i++)
            {
                var obj = new Permissions
                {
                    NombreEmpleado = $"Empleado_{i}",
                    ApellidoEmpleado = $"Apellido_{i}",
                    TipoPermiso = 0,
                    FechaPermiso = DateTime.UtcNow
                };
                listTemp.Add(obj);
            }

            return listTemp;
        }
    }
}
