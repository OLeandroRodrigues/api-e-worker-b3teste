using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Repositories;

namespace B3.Test.Data.Repositories
{
    public class RepositoryTarefaStatus : RepositoryBase<TarefaStatus>, IRepositoryTarefaStatus
    {
        public RepositoryTarefaStatus(B3TestContext Db)
        {
            this.Db = Db;
        }
    }
}
