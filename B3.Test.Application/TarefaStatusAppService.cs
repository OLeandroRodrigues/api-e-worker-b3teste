using B3.Test.Application.Interfaces;
using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;


namespace B3.Test.Application
{
    public class TarefaStatusAppService : AppServiceBase<TarefaStatus>, ITarefaStatusAppService
    {
        protected IServiceTarefaStatus _serviceTarefaStatus;
        private IConfiguration _configuration; 
        public TarefaStatusAppService(IServiceTarefaStatus serviceTarefaStatus, IConfiguration configuration) : base(serviceTarefaStatus)
        {
            _serviceTarefaStatus = serviceTarefaStatus;
            _configuration = configuration;
        }
    }
}
