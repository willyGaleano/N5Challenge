using Ardalis.Specification.EntityFrameworkCore;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Infraestructure.Persistence.Contexts;

namespace N5.Challenge.Infraestructure.Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly DbN5Context _dbN5Context;
        public MyRepositoryAsync(DbN5Context dbN5Context) : base(dbN5Context)
        {
            _dbN5Context = dbN5Context;
        }
    }
}
