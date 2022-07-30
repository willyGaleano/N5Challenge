using AutoMapper;
using MediatR;
using N5.Challenge.Core.Application.DTOs.PermissionTypes;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Core.Application.Wrappers;
using N5.Challenge.Core.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace N5.Challenge.Core.Application.Features.PermissionType.Queries.GetAllPermissionTypes
{
    public class GetAllPermissionTypesHandler : IRequestHandler<GetAllPermissionTypesQuery, Response<List<PermissionTypesDTO>>>
    {
        private readonly IRepositoryAsync<PermissionTypes> _repositoryPermissionTypes;
        private readonly IMapper _mapper;
        public GetAllPermissionTypesHandler(IRepositoryAsync<PermissionTypes> repositoryPermissionTypes, IMapper mapper)
        {
            _repositoryPermissionTypes = repositoryPermissionTypes;
            _mapper = mapper;
        }

        public async Task<Response<List<PermissionTypesDTO>>> Handle(GetAllPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resp = await _repositoryPermissionTypes.ListAsync(cancellationToken);
                var respDto = _mapper.Map<IEnumerable<PermissionTypesDTO>>(resp);
                return new Response<List<PermissionTypesDTO>>(respDto.OrderBy(x => x.PermissionTypeId).ToList(),"OK");
            }
            catch (Exception ex)
            {
                Log.Error($"GetAllPermissionTypesHandler method :: ex : {ex.Message}");
                return new Response<List<PermissionTypesDTO>>(ex.Message);
            }            
        }
    }
}
