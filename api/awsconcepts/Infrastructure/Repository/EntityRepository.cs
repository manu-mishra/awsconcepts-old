using Application.Common.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    internal class EntityRepository<Entity> : IEntityRepository<Entity>
    {
        public EntityRepository()
        {

        }
        public Task Delete(Entity DomainEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Entity> Get(string ScopeId, string Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entity>> GetAll(string ScopeId)
        {
            throw new NotImplementedException();
        }

        public Task<Entity> Put(Entity DomainEntity)
        {
            throw new NotImplementedException();
        }
    }
}
