using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Repositories;

namespace B3.Test.Data.Repositories
{
    public class RepositoryTarefa : RepositoryBase<Tarefa>, IRepositoryTarefa
    {
        public RepositoryTarefa(B3TestContext Db)
        {
            this.Db = Db;
        }
    }
}
