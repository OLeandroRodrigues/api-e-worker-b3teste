using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using B3.Test.Application.Interfaces;
using B3.Test.CrossCutting.API;
using B3.Test.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace B3.Test.API.Controllers
{
    public class TarefaStatusController : ApiController
    {
        protected ITarefaStatusAppService _tarefaStatusApp;
        protected IMapper _mapper;

        public TarefaStatusController(ITarefaStatusAppService tarefaStatusApp)
        {
            _tarefaStatusApp = tarefaStatusApp;
        }

        /// <summary>
        /// Obter Todas Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpGet("ObterTarefaStatus")]
        public async Task<ActionResult<TarefaStatus>> GetAllTarefaStatus()
        {
            try
            {
                var listaTarefasStatus = _tarefaStatusApp.GetAll();
                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, listaTarefasStatus));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
