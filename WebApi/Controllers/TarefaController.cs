using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        public readonly ITarefa _ITarafa;
        public readonly IMapper _IMapper;

        public TarefaController(ITarefa ITarefa, IMapper IMapper)
        {
            _ITarafa = ITarefa;
            _IMapper = IMapper;
        }

        [HttpPost("/AdicionarTarefa")]
        public async Task AdicionarTarefa(TarefaViewModel tarefa)
        {
            var tarefaMap = _IMapper.Map<Tarefa>(tarefa);

            await _ITarafa.AdicionarTarefa(tarefaMap);
        }

        [HttpPost("/Excluir")]
        public async Task Excluir(int idTarefa)
        {
            await _ITarafa.Excluir(new Tarefa { Id = idTarefa });
        }

        [HttpPost("/BuscarTarefa")]
        public async Task<TarefaViewModel> BuscarTarefa(int idTarefa)
        {
            var tarefa = await _ITarafa.BuscarTarefa(idTarefa);
            var clienteMap = _IMapper.Map<TarefaViewModel>(tarefa);
            return clienteMap;
        }

        [HttpPost("/ListarTarefas")]
        public async Task<List<TarefaViewModel>> ListarTarefas()
        {
            var tarefa = await _ITarafa.ListarTarefas();
            var clienteMap = _IMapper.Map<List<TarefaViewModel>>(tarefa);
            return clienteMap;
        }

    }
}
