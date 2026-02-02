using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRelatosBugs.Infrastructure;
using SistemaRelatosBugs.Web.Models.ViewModels;
using System.Linq;

namespace SistemaRelatosBugs.Web.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class ManagerController : Controller
    {
        private readonly AppDbContext _db;

        public ManagerController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var tickets = _db.Tickets
                .Include(t => t.Relator)
                .Select(t => new TicketListItemViewModel
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    RelatorNome = t.Relator != null ? t.Relator.Nome : "-",
                    Tipo = t.Tipo,
                    Empreendimento = t.Empreendimento,
                    Status = t.Status,
                    DataCriacao = t.DataCriacao
                })
                .ToList();

            var vm = new ManagerDashboardViewModel { Tickets = tickets };
            return View(vm);
        }
    }
}
