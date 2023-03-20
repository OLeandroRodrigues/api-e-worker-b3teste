using System;
using B3.Test.Application.Interfaces;
using B3.Test.Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using B3.Test.Domain.ViewModel;
using B3.Test.Domain.Validations;
using B3.Test.CrossCutting.API;
using AutoMapper;
using B3.Test.RabbitMq.Abstractions;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace B3.Test.API.Controllers
{
    public class TarefaController : ApiController
    {
        protected ITarefaAppService _tarefaApp;
        protected ITarefaStatusAppService _tarefaStatusApp;
        protected IMapper _mapper;
        public IConfiguration _config;

        public TarefaController(ITarefaAppService tarefaApp, ITarefaStatusAppService tarefaStatusApp, IConfiguration config, IMapper mapper)
        {
            _tarefaApp = tarefaApp;
            _tarefaStatusApp = tarefaStatusApp;
            _mapper = mapper;
            _config = config;
        }

        /// <summary>
        /// Incluir Tarefa na Fila no RabbitMq
        /// </summary>
        /// <returns>tarefa incluída</returns>
        [HttpPost("TarefaFila")]
        public async Task<ActionResult<Tarefa>> TarefaFila(TarefaCadastroViewModel tarefaViewModel)
        {
            try
            {
                if (!IsEntityValid(new TarefaValidator(), tarefaViewModel, out string[] erros))
                    return ResponseMessage(HttpStatusCode.BadRequest, erros);

                var tarefasStatus = _tarefaStatusApp.GetAll();
                if (!tarefasStatus.Any(p => p.TarefaStatusId == tarefaViewModel.TarefaStatusId))
                    return ResponseMessage(HttpStatusCode.BadRequest, "Identificação de Tarefa Status inválido.");

                B3RabbitMq d = new B3RabbitMq();

                d.InitiateConnectionFactory(_config.GetSection("RabbitMq:ProviverConnection").Value);

                var message = new { Name = "Producer: Tarefa Status", Message = tarefaViewModel };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                d.CreateItemOnQueue(_config.GetSection("RabbitMq:CadastroTarefaQueue").Value, body);
                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, tarefaViewModel));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        /// <summary>
        /// Cadastrar Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpPost("Tarefa")]
        public async Task<ActionResult<Tarefa>> Tarefa(TarefaCadastroViewModel tarefaViewModel)
        {
            try
            {
                if (!IsEntityValid(new TarefaValidator(), tarefaViewModel, out string[] erros))
                    return ResponseMessage(HttpStatusCode.BadRequest, erros);

                var tarefasStatus = _tarefaStatusApp.GetAll();
                if (!tarefasStatus.Any(p => p.TarefaStatusId == tarefaViewModel.TarefaStatusId))
                    return ResponseMessage(HttpStatusCode.BadRequest, "Identificação de Tarefa Status inválido.");

                var tarefa = _mapper.Map<TarefaCadastroViewModel, Tarefa>(tarefaViewModel);
                _tarefaApp.Add(tarefa);

                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, tarefa));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        /// <summary>
        /// Cadastrar Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpDelete("Tarefa")]
        public async Task<ActionResult<Tarefa>> DeleteTarefa(int tarefaId)
        {
            try
            {
                var tarefa = _tarefaApp.GetById(tarefaId);
                if (tarefa.TarefaId == 0)
                    return ResponseMessage(HttpStatusCode.BadRequest, "Identificação de Tarefa Status inválido.");

                _tarefaApp.Remove(tarefa);

                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, tarefa));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Cadastrar Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpPut("Tarefa")]
        public async Task<ActionResult<Tarefa>> UpdateTarefa(TarefaUpdateViewModel tarefaViewModel)
        {
            try
            {
                if (!IsEntityValid(new TarefaValidator(), tarefaViewModel, out string[] erros))
                    return ResponseMessage(HttpStatusCode.BadRequest, erros);

                var tarefasStatus = _tarefaStatusApp.GetAll();
                if (!tarefasStatus.Any(p => p.TarefaStatusId == tarefaViewModel.TarefaStatusId))
                    return ResponseMessage(HttpStatusCode.BadRequest, "Identificação de Tarefa Status inválido.");

                var tarefa = _mapper.Map<TarefaUpdateViewModel, Tarefa>(tarefaViewModel);
                _tarefaApp.Update(tarefa);

                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, tarefa));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Obter Todas Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpGet("GetAllTarefa")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetAllTarefa()
        {
            try
            {
                var listaTarefas = _tarefaApp.GetAll();
                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, listaTarefas));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Obter Todas Tarefa
        /// </summary>
        /// <returns>tarefa cadastrada</returns>
        [HttpGet("GetTarefaById")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefaById(int tarefaId)
        {
            try
            {
                var tarefa = _tarefaApp.GetById(tarefaId);
                return await Task.FromResult(ResponseMessage(HttpStatusCode.OK, tarefa));
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                return await Task.FromResult(ResponseMessage(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
