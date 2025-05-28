
using Dominio.Entidades;
using Infraestrutura.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infraestrutura.EntityFramework
{
    public class Context : DbContext
    {
        private IConfiguration _configuration;
        public Context(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if(!DEBUG)
            string connetionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
#else
            string connetionString = _configuration.GetConnectionString("DefaultConnection");
#endif

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ApplyConfiguration(new NarrativaMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }

        public DbSet<Narrativa> Narrativas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}