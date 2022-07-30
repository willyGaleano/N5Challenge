using Ardalis.Specification;

namespace N5.Challenge.Core.Application.Interfaces.Repository
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class { }
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class { }
}
