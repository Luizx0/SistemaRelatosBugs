using System;
using SistemaRelatosBugs.Domain;

namespace SistemaRelatosBugs.Web.Models.ViewModels
{
    public class TicketListItemViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string RelatorNome { get; set; }
        public TipoTicket Tipo { get; set; }
        public string Empreendimento { get; set; }
        public StatusTicket Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
