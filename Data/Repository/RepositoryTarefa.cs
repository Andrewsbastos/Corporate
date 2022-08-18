using Data.Config;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryTarefa : RepositoryGeneric<Tarefa>, ITarefa
    {
        private readonly DbContextOptions<ContextBase> _optionsBulder;

        public RepositoryTarefa()
        {
            _optionsBulder = new DbContextOptions<ContextBase>();
        }

        public async Task AdicionarTarefa(Tarefa Objeto)
        {
            using (var data = new ContextBase(_optionsBulder))
            {
                await data.Set<Tarefa>().AddAsync(Objeto);
                await data.SaveChangesAsync();

                if (Objeto.ItensTarefa.Any())
                {
                    Objeto.ItensTarefa.ForEach(a => a.IdTarefa = Objeto.Id);

                    await data.Set<ItemTarefa>().AddRangeAsync(Objeto.ItensTarefa);
                    await data.SaveChangesAsync();
                }

            }
        }

        public async Task<Tarefa> BuscarTarefa(int Id)
        {
            using (var data = new ContextBase(_optionsBulder))
            {

                var tarefa = await data.Tarefa.FindAsync(Id);
                if (tarefa != null)
                {
                    var itensTarefa = await data.ItemTarefa.Where(a => a.IdTarefa.Equals(Id)).ToListAsync();

                    if (itensTarefa.Any())
                        tarefa.ItensTarefa = itensTarefa;

                    return tarefa;
                }
                else return null;
            }
        }

        public async Task<List<Tarefa>> ListarTarefas()
        {
            var listaTarefas = new List<Tarefa>();

            using (var data = new ContextBase(_optionsBulder))
            {
                var tarefaComItens = await (from TA in data.Tarefa
                                            join ITA in data.ItemTarefa on TA.Id equals ITA.IdTarefa
                                            select new
                                            {
                                                Id = TA.Id,
                                                Nome = TA.Nome,
                                                IdItemTarefa = ITA.Id,
                                                ItemTarefaNome = ITA.Nome,
                                                ITA.IdTarefa,
                                                ITA.Observacao
                                            }).ToListAsync();

                var lista = tarefaComItens.Select(a => new { Id = a.Id, Nome = a.Nome }).Distinct().ToList();

                var listaCompleta = lista.Select(a => new Tarefa
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    ItensTarefa =
        tarefaComItens.Where(x => x.IdTarefa == a.Id)
        .Select(x => new ItemTarefa { Id = x.IdItemTarefa, Nome = x.ItemTarefaNome, IdTarefa = x.IdTarefa, Observacao = x.Observacao }).ToList()
                });

                if (listaCompleta.Any())
                    listaTarefas.AddRange(listaCompleta);
            }

            return listaTarefas;
        }
    }
}