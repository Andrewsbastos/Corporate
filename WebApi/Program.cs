using AutoMapper;
using Data.Config;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using WebApi.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextBase>
  (options => options.UseSqlServer("Server=RAPHA\\SQLEXPRESS; Database=CorpDb;Trusted_Connection=True"));

builder.Services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGeneric<>));
builder.Services.AddSingleton<ITarefa, RepositoryTarefa>();
builder.Services.AddSingleton<InterfaceItemTarefa, RepositoryItemTarefa>();

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<TarefaViewModel, Tarefa>();
    cfg.CreateMap<Tarefa, TarefaViewModel>();

    cfg.CreateMap<ItemTarefaViewModel, ItemTarefa>();
    cfg.CreateMap<ItemTarefa, ItemTarefaViewModel>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();