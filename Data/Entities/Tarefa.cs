using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Tarefa")]
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [NotMapped]
        public List<ItemTarefa> ItensTarefa { get; set; }
    }
}