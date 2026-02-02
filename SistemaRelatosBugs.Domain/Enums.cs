using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaRelatosBugs.Domain
{
public enum TipoUsuario
{
    Gestor = 1,
    Relator = 2
}

public enum TipoTicket
{
    Bug = 1,
    Falha = 2
}

public enum StatusTicket
{
    Aberto = 1,
    EmAnalise = 2,
    Resolvido = 3
}

}