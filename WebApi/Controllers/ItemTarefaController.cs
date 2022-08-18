using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTarefaController : ControllerBase
    {
        public readonly InterfaceItemTarefa _InterfaceItemTarefa;

        public ItemTarefaController(InterfaceItemTarefa InterfaceItemTarefa)
        {
            _InterfaceItemTarefa = InterfaceItemTarefa;
        }

        [HttpPost("/AdicionarItem")]
        public async Task AdicionarItem(ItemTarefa itemTarefa)
        {
            await _InterfaceItemTarefa.Adicionar(itemTarefa);
        }

        [HttpPost("/AtualizarItem")]
        public async Task AtualizarItem(ItemTarefa itemTarefa)
        {
            await _InterfaceItemTarefa.Atualizar(itemTarefa);
        }

        [HttpPost("/ExcluirItem")]
        public async Task ExcluirItem(ItemTarefa itemTarefa)
        {
            await _InterfaceItemTarefa.Excluir(itemTarefa);
        }

        [HttpPost("/BuscaPorCodigoItem")]
        public async Task<ItemTarefa> BuscaPorCodigoItem(int idItemTarefa)
        {
            return await _InterfaceItemTarefa.BuscaPorCodigo(idItemTarefa);
        }


        [HttpPost("/ListarTodosItem")]
        public async Task<List<ItemTarefa>> ListarTodosItem()
        {
            return await _InterfaceItemTarefa.ListarTudo();
        }

    }
}