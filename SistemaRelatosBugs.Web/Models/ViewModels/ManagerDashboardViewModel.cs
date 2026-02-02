using System.Collections.Generic;

namespace SistemaRelatosBugs.Web.Models.ViewModels
{
    public class ManagerDashboardViewModel
    {
        public List<TicketListItemViewModel> Tickets { get; set; } = new List<TicketListItemViewModel>();
    }
}
