# 🐛 Sistema de Relatos de Bugs

Um sistema web moderna e responsivo para gerenciamento de relatos de bugs e falhas, desenvolvido com ASP.NET Core e arquitetura MVVM (Model-View-ViewModel).

## 📋 Visão Geral

O Sistema de Relatos de Bugs é uma aplicação que permite:
- **Gestores**: Visualizar e gerenciar todos os tickets reportados no sistema
- **Relatores**: Abrir novos tickets de bugs/falhas e acompanhar seu status

## 🏗️ Arquitetura MVVM

O projeto está organizado seguindo o padrão **MVVM (Model-View-ViewModel)**:

### Camadas da Aplicação

```
SistemaRelatosBugs/
├── SistemaRelatosBugs.Domain/          # Modelos de domínio
│   ├── models/
│   │   ├── Usuario.cs                   # Entidade de usuário
│   │   └── Ticket.cs                    # Entidade de ticket
│   └── Enums.cs                         # Enumerações (Tipo, Status, etc)
│
├── SistemaRelatosBugs.Infrastructure/   # Acesso a dados
│   ├── Data/
│   │   └── AppDbContext.cs              # DbContext do Entity Framework
│   └── Seed/
│       └── DbInitializer.cs             # Seed de dados iniciais
│
├── SistemaRelatosBugs.Application/      # Lógica de negócio
│   └── TicketService.cs                 # Serviço de tickets
│
└── SistemaRelatosBugs.Web/              # Apresentação (Views, Controllers)
    ├── Controllers/
    │   ├── AccountController.cs         # Autenticação
    │   ├── ManagerController.cs         # Dashboard do Gestor
    │   └── RelatorController.cs         # Dashboard do Relator
    ├── Models/ViewModels/
    │   ├── LoginViewModel.cs
    │   ├── TicketListItemViewModel.cs
    │   ├── ManagerDashboardViewModel.cs
    │   ├── RelatorDashboardViewModel.cs
    │   └── TicketCreateViewModel.cs
    └── Views/
        ├── Account/Login.cshtml
        ├── Manager/Index.cshtml
        ├── Relator/Index.cshtml
        ├── Relator/Create.cshtml
        └── Shared/_Layout.cshtml
```

### Fluxo MVVM

1. **Model**: Entidades do domínio (`Usuario`, `Ticket`) + ViewModels especializados
2. **ViewModel**: Classes que preparam dados para as views (`ManagerDashboardViewModel`, etc)
3. **View**: Templates Razor que renderizam a interface para o usuário
4. **Controller**: Orquestra requisições, interage com a aplicação e retorna views

## 🚀 Como Executar

### Pré-requisitos
- .NET 9.0 SDK instalado
- Qualquer editor de texto (VS Code, Visual Studio, etc)

### Passos

1. **Abra o terminal** e navegue para a pasta do projeto Web:
```powershell
cd D:\Luizx\!Program\SistemaRelatosBugs\SistemaRelatosBugs.Web
```

2. **Execute a aplicação**:
```powershell
dotnet run
```

3. **Acesse no navegador**:
   - Abra [http://localhost:5098](http://localhost:5098)
   - Você será automaticamente redirecionado para a tela de login

## 👥 Usuários de Teste

### Gestor
- **Login**: `admin`
- **Senha**: `123`
- **Acesso**: Visualiza todos os tickets do sistema em um dashboard

### Relator
- **Login**: `joao`
- **Senha**: `123`
- **Acesso**: Cria tickets e acompanha seu status

## 📱 Funcionalidades

### Tela de Login
- Campo único de acesso (aceita email, CPF ou username)
- Interface moderna com gradiente
- Validação básica de credenciais
- Redirecionamento automático por role (Gestor → Manager Dashboard, Relator → Relator Dashboard)

### Dashboard do Gestor (Manager)
- Listagem de todos os tickets do sistema
- Colunas: #ID, Título, Relator, Tipo (Bug/Falha), Empreendimento, Data
- Tabela com hover effects
- Badges coloridas para tipos de ticket
- Interface responsiva

### Dashboard do Relator
- Listagem de tickets abertos pelo usuário
- Status visual de cada ticket (Aberto, Em Análise, Resolvido)
- Botão para abrir novo ticket
- Acesso somente-leitura (sem edição)

### Formulário de Criar Ticket
- Campo de Título
- Campo de Descrição (textarea)
- Seletor de Tipo (Bug/Falha)
- Campo de Empreendimento/Módulo
- Validação de campos obrigatórios

## 🔐 Autenticação

O sistema utiliza **autenticação por Cookie (ASP.NET Core Identity-like)**:
- Cookies de segurança
- Role-based access control (Gestor vs Relator)
- Logout seguro com invalidação de sessão
- Acesso protegido com `[Authorize]`

## 🎨 Design e UI

- **Framework**: Bootstrap 5
- **Cores**: Gradiente roxo (#667eea → #764ba2)
- **Responsive**: Mobile-first, funciona em qualquer dispositivo
- **Emojis**: Ícones visuais para melhor UX
- **Badges**: Indicadores visuais de tipo e status

## 💾 Banco de Dados

- **Tipo**: In-Memory (EntityFramework Core InMemory Provider)
- **ORM**: Entity Framework Core 7.0
- **Dados Iniciais**: Automaticamente seedados ao iniciar a aplicação
  - 1 Gestor
  - 1 Relator
  - 2 Tickets de exemplo

## 🔧 Configuração

### Program.cs
```csharp
// Autenticação por Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// DbContext em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SistemaRelatosBugsDB"));
```

## 📊 Modelos de Dados

### Usuario
```csharp
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Username { get; set; }
    public string SenhaHash { get; set; }
    public TipoUsuario Tipo { get; set; }
    public List<Ticket> TicketsCriados { get; set; }
}
```

### Ticket
```csharp
public class Ticket
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public TipoTicket Tipo { get; set; }  // Bug ou Falha
    public string Empreendimento { get; set; }
    public StatusTicket Status { get; set; }  // Aberto, EmAnalise, Resolvido
    public int RelatorId { get; set; }
    public Usuario Relator { get; set; }
    public DateTime DataCriacao { get; set; }
}
```

### Enumerações
```csharp
public enum TipoUsuario { Gestor = 1, Relator = 2 }
public enum TipoTicket { Bug = 1, Falha = 2 }
public enum StatusTicket { Aberto = 1, EmAnalise = 2, Resolvido = 3 }
```

## 🚦 Fluxo de Autenticação

```
1. Usuário acessa /Account/Login
2. Digita Login (email/CPF/username) + Senha
3. Sistema valida credenciais no banco (campo "Login" com fallback para Email/CPF/Username)
4. Se válido:
   - Cria claim com Role (Gestor/Relator)
   - Gera cookie de autenticação
   - Redireciona para respectivo dashboard
5. Se inválido:
   - Exibe mensagem de erro
   - Solicita novo login
```

## 📝 Notas de Desenvolvimento

- O sistema usa In-Memory Database para simplicidade. Para produção, configure um banco real no `Program.cs`
- Todos os dados são perdidos ao reiniciar a aplicação (característica do In-Memory)
- A autenticação é simplificada; em produção use Identity/OAuth
- Adicione validações reais (DataAnnotations, FluentValidation) antes de usar em produção

## 👨‍💻 Estrutura de Pastas Recomendada para Crescimento

```
SistemaRelatosBugs/
├── SistemaRelatosBugs.Domain/
│   ├── Entities/              # Modelos do domínio
│   ├── Enums/
│   ├── Interfaces/            # Contracts
│   └── ValueObjects/          # Objetos de valor
├── SistemaRelatosBugs.Application/
│   ├── Services/              # Lógica de negócio
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Interfaces/
│   └── Mappings/              # AutoMapper profiles
├── SistemaRelatosBugs.Infrastructure/
│   ├── Data/                  # DbContext
│   ├── Repositories/          # Padrão Repository
│   ├── Seed/
│   └── Migrations/            # EF Migrations
├── SistemaRelatosBugs.Web/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── Filters/               # Action filters
│   ├── Middleware/
│   └── wwwroot/               # Assets (CSS, JS, images)
└── SistemaRelatosBugs.Tests/   # Testes unitários e integração
```

## 🔗 Recursos Úteis

- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Bootstrap 5](https://getbootstrap.com)
- [MVVM Pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel)

## 📄 Licença

Este projeto é fornecido como exemplo educacional.

---

**Versão**: 1.0  
**Última atualização**: Fevereiro 2026  
**Status**: ✅ Funcional e pronto para uso
