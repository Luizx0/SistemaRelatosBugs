using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRelatosBugs.Infrastructure;
using SistemaRelatosBugs.Web.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using SistemaRelatosBugs.Domain;
using SistemaRelatosBugs.Domain.models;

namespace SistemaRelatosBugs.Web.Controllers
{
    [Authorize(Roles = "Relator")]
    public class RelatorController : Controller
    {
        private readonly AppDbContext _db;

        public RelatorController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var tickets = _db.Tickets
                .Where(t => t.RelatorId == userId)
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

            var vm = new RelatorDashboardViewModel { Tickets = tickets };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TicketCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(TicketCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var ticket = new Ticket
            {
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                Tipo = model.Tipo,
                Empreendimento = model.Empreendimento,
                Status = StatusTicket.Aberto,
                RelatorId = userId,
                DataCriacao = System.DateTime.UtcNow
            };

            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
