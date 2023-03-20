using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace B3.Test.Data
{
    public class B3TestContext : DbContext
    {
        public IConfiguration _config;
        public B3TestContext(DbContextOptions<B3TestContext> options) : base(options)
        {
            
        }

        private void RegisterMaps(ModelBuilder builder)
        {
            var maps = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace)
                && ( typeof(TarefaMap).IsAssignableFrom(type) 
                || typeof(TarefaStatusMap).IsAssignableFrom(type)) 
                && type.IsClass).ToList();

            foreach (var item in maps)
                if (item.Name != "IEntityMap")
                {
                    Activator.CreateInstance(item, BindingFlags.Public |
                    BindingFlags.Instance, null, new object[] { builder }, null);
                }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO : ALTERAR PARA PEGAR DA CONFIGURAÇÃO  
            var connetionString = "server=localhost;port=3390;userid=root;password=admin;database=b3teste_db;";
            optionsBuilder.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterMaps(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
