using Microsoft.EntityFrameworkCore;
using SistemaRelatosBugs.Domain;
using SistemaRelatosBugs.Domain.models;

namespace SistemaRelatosBugs.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
