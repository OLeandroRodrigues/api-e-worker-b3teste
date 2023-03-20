using B3.Test.Application.Interfaces;
using B3.Test.Data;
using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;


namespace B3.Test.Application
{
    public class TarefaAppService : AppServiceBase<Tarefa>, ITarefaAppService
    {
        protected IServiceTarefa _serviceTarefa;
        private IConfiguration _configuration; 
        public TarefaAppService(IServiceTarefa serviceTarefa, IConfiguration configuration) : base(serviceTarefa)
        {
            _serviceTarefa = serviceTarefa;
            _configuration = configuration;
        }
    }
}
