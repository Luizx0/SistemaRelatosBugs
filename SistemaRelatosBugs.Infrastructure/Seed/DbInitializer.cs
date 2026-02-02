using SistemaRelatosBugs.Domain;
using SistemaRelatosBugs.Domain.models;

namespace SistemaRelatosBugs.Infrastructure;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Usuarios.Any()) return;

        var gestor = new Usuario { Nome = "Gestor Admin", Login = "admin", Username = "gestor", Email = "gestor@local", CPF = "00000000000", SenhaHash = "123", Tipo = TipoUsuario.Gestor };
        var relator = new Usuario { Nome = "Relator Luiz", Login = "luiz", Username = "luiz", Email = "luiz@local", CPF = "11111111111", SenhaHash = "123", Tipo = TipoUsuario.Relator };

        context.Usuarios.AddRange(gestor, relator);
        context.SaveChanges();

        context.Tickets.AddRange(
            new Ticket { Titulo = "Erro no login", Descricao = "Não consigo logar no sistema", Tipo = TipoTicket.Bug, Empreendimento = "Portal", Status = StatusTicket.Aberto, RelatorId = relator.Id, DataCriacao = DateTime.UtcNow },
            new Ticket { Titulo = "Falha ao salvar", Descricao = "Salvar formulário retorna erro", Tipo = TipoTicket.Falha, Empreendimento = "Admin", Status = StatusTicket.Aberto, RelatorId = relator.Id, DataCriacao = DateTime.UtcNow }
        );

        context.SaveChanges();
    }
}
