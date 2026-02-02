using SistemaRelatosBugs.Domain;

namespace SistemaRelatosBugs.Web.Models.ViewModels
{
    public class TicketCreateViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public TipoTicket Tipo { get; set; }
        public string Empreendimento { get; set; }
    }
}
