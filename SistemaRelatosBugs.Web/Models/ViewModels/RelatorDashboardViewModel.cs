using System.Collections.Generic;

namespace SistemaRelatosBugs.Web.Models.ViewModels
{
    public class RelatorDashboardViewModel
    {
        public List<TicketListItemViewModel> Tickets { get; set; } = new List<TicketListItemViewModel>();
        public bool CanCreate { get; set; } = true;
    }
}
