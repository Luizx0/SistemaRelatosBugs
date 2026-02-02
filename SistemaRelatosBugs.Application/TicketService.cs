using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SistemaRelatosBugs.Domain;
using SistemaRelatosBugs.Domain.models;
using SistemaRelatosBugs.Infrastructure;

namespace SistemaRelatosBugs.Application;

public class TicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public List<Ticket> ListarTodosTickets()
    {
        return _context.Tickets
            .Include(t => t.Relator)
            .ToList();
    }

    public List<Ticket> ListarTicketsDoRelator(int relatorId)
    {
        return _context.Tickets
            .Where(t => t.RelatorId == relatorId)
            .ToList();
    }

    public void CriarTicket(Ticket ticket)
    {
        ticket.Status = StatusTicket.Aberto;
        ticket.DataCriacao = DateTime.Now;

        _context.Tickets.Add(ticket);
        _context.SaveChanges();
    }
}
