using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITarefa : IGeneric<Tarefa>
    {
        Task AdicionarTarefa(Tarefa Objeto);

        Task<Tarefa> BuscarTarefa(int Id);

        Task<List<Tarefa>> ListarTarefas();
    }
}
