using ControleContatos2022.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos2022.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get;set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
