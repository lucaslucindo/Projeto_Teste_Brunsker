using ImoveisLocacao.Models;
using Microsoft.EntityFrameworkCore;

namespace ImoveisLocacao.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<LocacaoImovel> LocacoesImoveis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
        
}
