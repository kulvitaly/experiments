# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**BudgetManager** is a family budget management web application. Multiple family members share a single family account and can record expenses and incomes, manage accounts/cards, transfer money between accounts, and plan spending by category.

Core capabilities:
- **Transactions** — record expenses and incomes, each linked to an account and a category
- **Accounts** — multiple accounts/cards per family (bank card, cash, savings, etc.)
- **Transfers** — move money between accounts (recorded as a paired debit + credit, not as expense/income)
- **Categories** — separate category trees for expenses and incomes
- **Budgets** — spending limits per expense category per period; UI warns when approaching or exceeding a limit
- **Multi-user** — family members (e.g. husband, wife, daughter) share one family space; each transaction records which user created it

The app is browser-first today, with a future mobile app in mind — keep the API contract clean and stateless.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 10, ASP.NET Core |
| API | GraphQL via Hot Chocolate |
| ORM | Entity Framework Core |
| Mapping | Mapperly (source-generated mappers) |
| Database | PostgreSQL |
| Frontend | React, TypeScript |
| Auth | TBD — must support multiple named users under one family account |

## Domain Model (conceptual)

```
Family
 ├── Users[]           (family members, each with name + role)
 ├── Accounts[]        (card/cash/savings; has balance, currency, name)
 ├── ExpenseCategories[]   (hierarchical, e.g. Food > Groceries)
 ├── IncomeCategories[]    (e.g. Salary, Freelance, Gift)
 ├── Transactions[]
 │    ├── Expense      (amount, date, account, expenseCategory, user, note)
 │    ├── Income       (amount, date, account, incomeCategory, user, note)
 │    └── Transfer     (fromAccount, toAccount, amount, date, user, note)
 └── BudgetPlans[]     (expenseCategory, period [monthly/weekly], limitAmount)
```

**Balance** on an account is always derived from its transactions — never stored as a mutable field (or maintained via EF Core computed + event-sourcing approach TBD).

**Budget progress** = sum of expenses in a category for the current period vs. the plan limit. Status: OK / Warning (e.g. >80%) / Exceeded.

## Project Structure

```
BudgetManager/
├── backend/
│   ├── src/
│   │   ├── BudgetManager.Domain/         # Entities, value objects, domain events
│   │   ├── BudgetManager.Application/    # Use cases, MediatR commands/queries, interfaces
│   │   ├── BudgetManager.Infrastructure/ # EF Core DbContext, migrations, repositories
│   │   └── BudgetManager.Api/            # ASP.NET Core entry point, DI setup, endpoints
│   └── tests/
│       └── BudgetManager.*.Tests/        # xUnit — one project per layer
├── frontend/
│   ├── src/
│   │   ├── api/          # Typed API client (fetch / axios wrappers)
│   │   ├── components/   # Shared UI components
│   │   ├── features/     # Feature folders: transactions, accounts, categories, budget
│   │   ├── hooks/        # Custom React hooks
│   │   └── types/        # Shared TypeScript types mirroring backend DTOs
│   └── package.json
└── docker-compose.yml    # PostgreSQL (and any other local services)
```

## Build & Test Commands

### Backend
```bash
cd backend

dotnet restore
dotnet build --configuration Release
dotnet test
dotnet test --filter "FullyQualifiedName~TestMethodName"   # single test

# Run API locally
dotnet run --project src/BudgetManager.Api
```

### Frontend
```bash
cd frontend

npm install
npm run dev       # dev server
npm run build     # production build
npm run lint      # ESLint
npm test          # Jest / Vitest
```

### Local dev environment
```bash
docker-compose up -d   # start PostgreSQL
```

## Code Style

- **No `Async` suffix** — do not add `Async` to async method names.
- **Model-first GraphQL** — for each aggregate exposed via GraphQL, define an `ObjectType<T>` (e.g. `CategoryObjectType : ObjectType<Category>`) in `BudgetManager.Api/GraphQL/{Feature}/` and register it with `.AddType<>()` before adding queries or mutations that return the aggregate.
- **FluentAssertions in tests** — use FluentAssertions (`.Should().Be(...)`, `.Should().NotBeEmpty()`, etc.) instead of `Assert.*` in all test projects.

## CQRS Conventions

- Queries and commands return **domain entities directly** — no DTOs.
- Handlers are `internal sealed` and live in the same file as their query/command record.
- **Never call `SaveChangesAsync` in a handler** — `TransactionBehaviour` in the pipeline calls it automatically after every command.
- Use `IQuery<TResponse>` for reads and `ICommand<TResponse>` for writes (both in `BudgetManager.Application.Common.MediatR`).

## EF Core Conventions

- **Typed ID value converters** — all `Key<Guid>` ID types need explicit conversion in `IEntityTypeConfiguration<T>`:
  ```csharp
  builder.Property(e => e.Id).HasConversion(id => id.Value, value => new EntityId(value));
  ```
- **Enum columns** — store as string with `.HasConversion<string>()` to avoid ordinal drift.
- **Collections not yet mapped** — use `builder.Ignore(e => e.Collection)` until the related aggregate has its own `DbSet`.
- EF Core migrations require the `dotnet-ef` global tool: `dotnet tool install --global dotnet-ef`.

## GraphQL Conventions

- GraphQL resolver methods take **`Guid` parameters** (not typed IDs) — construct the typed ID when dispatching to MediatR:
  ```csharp
  mediator.Send(new GetIncomeCategoriesQuery(new FamilyId(familyId)), cancellationToken)
  ```
- Annotate resolver return types explicitly:
  - Single entity: `[GraphQLType(typeof(FooObjectType))]`
  - Collection: `[GraphQLType(typeof(ListType<FooObjectType>))]`
- In `ObjectType<T>`, unwrap typed IDs to `Guid` via `.Resolve(ctx => ctx.Parent<T>().Id.Value)`.

## Key Architectural Decisions

- **Clean Architecture** — Domain has zero external dependencies; Application depends only on Domain; Infrastructure implements Application interfaces.
- **CQRS via MediatR** — every use case is a `IRequest` handler; GraphQL resolvers are thin dispatchers.
- **GraphQL via Hot Chocolate** — queries, mutations, and subscriptions defined in `BudgetManager.Api`; use Hot Chocolate's EF Core integration for efficient data fetching (projections, filtering, sorting).
- **Mapperly** — source-generated mappers (no reflection); define mapper interfaces in `BudgetManager.Application` and implement in the same or `Infrastructure` layer.
- **EF Core migrations** live in `BudgetManager.Infrastructure`; run with `dotnet ef migrations add <Name> --project src/BudgetManager.Infrastructure --startup-project src/BudgetManager.Api`.
- **Transfers are not expenses/income** — they are first-class transaction types so they don't skew category reports or budget calculations.
- **Frontend feature folders** — each feature (transactions, accounts, budget) owns its pages, components, hooks, and API calls; avoid a flat component soup.
- **Mobile-friendly API** — GraphQL endpoint is stateless; the same schema will serve a future mobile client. Use persisted queries or code-generated typed hooks (e.g. graphql-codegen) on the frontend.
