using Ardalis.Specification;
using N5.Challenge.Core.Domain.Entities;
using System.Linq;

namespace N5.Challenge.Core.Application.Specifications.Permissions
{
    public class GetByNameSpecification : Specification<PermissionTypes>, ISingleResultSpecification
    {
        public GetByNameSpecification(string description)
        {
            Query.Where(x => x.Descripcion.ToUpper() == description.ToUpper());
        }
    }
}
