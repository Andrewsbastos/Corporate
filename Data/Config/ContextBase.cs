using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<ItemTarefa> ItemTarefa { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConect());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string GetStringConect()
        {
            return "Server=RAPHA\\SQLEXPRESS; Database=CorpDb; Trusted_Connection=True";
        }

    }
}