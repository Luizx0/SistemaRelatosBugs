using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaRelatosBugs.Domain.models
{
public class Usuario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Login { get; set; } // email, cpf ou username (campo de acesso alternativo)
    public string? Email { get; set; }
    public string? CPF { get; set; }
    public string? Username { get; set; }
    public string? SenhaHash { get; set; }
    public TipoUsuario Tipo { get; set; }

    public List<Ticket> TicketsCriados { get; set; } = new List<Ticket>();
}
}