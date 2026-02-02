using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaRelatosBugs.Domain.models
{
public class Ticket
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public TipoTicket Tipo { get; set; } // Bug ou Falha
    public string? Empreendimento { get; set; }
    public StatusTicket Status { get; set; }

    public int RelatorId { get; set; }
    public Usuario? Relator { get; set; }

    public DateTime DataCriacao { get; set; }
}

}