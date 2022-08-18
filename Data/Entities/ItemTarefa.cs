using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{

    [Table("ItemTarefa")]

    public class ItemTarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        [ForeignKey("Tarefa")]
        [Column(Order = 1)]
        public int IdTarefa { get; set; }
    }
}