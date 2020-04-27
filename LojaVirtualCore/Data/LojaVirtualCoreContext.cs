using LojaVirtualCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualCore.Data
{
    public class LojaVirtualCoreContext : DbContext
    {
        public LojaVirtualCoreContext(DbContextOptions<LojaVirtualCoreContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
    }
}
