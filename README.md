# Library Management System (LMS)

A modular console application built with C# and .NET 10, designed as a learning project
to practice Clean Architecture, Dependency Injection, and SOLID principles.

---

## Architecture

The solution is split into four projects, each with a single responsibility.
Dependencies flow inward — outer layers know about inner ones, never the reverse.

LMS.Presentation
└── LMS.Infrastructure
└── LMS.Application
└── LMS.Domain

### LMS.Domain
The core of the system. Contains business entities, interfaces, and enums.
Has zero external dependencies — no JSON, no UI, no framework.

- `Book` — entity with title, author, and quantity validation
- `Member` — entity with name, email, date of birth, and regex email validation
- `Transaction` — immutable entity representing a borrow or return event
- `TransactionType` — enum (Borrow, Return)
- `IBookRepository` — contract for book persistence
- `IMemberRepository` — contract for member persistence
- `ITransactionRepository` — contract for transaction persistence
- `IBookAvailabilityService` — contract for available copies calculation

### LMS.Application
Orchestration layer between Domain and Infrastructure.

- `BookAvailabilityService` — calculates available copies using Quantity minus active borrows,
  derived from transaction history

### LMS.Infrastructure
Implements the persistence contracts defined in Domain using JSON files.

- `JsonBookRepository` → `library_data.json`
- `JsonMemberRepository` → `members_data.json`
- `JsonTransactionRepository` → `transactions_data.json`

### LMS.Presentation
Entry point and console UI. Wires up the DI container in `LMS.cs` and routes
user input through `MainMenu`, `BookUI`, and `MemberUI`.

---

## Features

### Books
- Add a new book with title, author, and quantity
- View all books with available copies calculated from transaction history
- Delete a book by ID

### Members
- Register a new member with name, surname, email, and date of birth
- View all members
- Delete a member by ID

### Transactions
- Checkout a book (member borrows a copy)
- Return a book (member returns an active loan)
- Query transactions by book or member

---

## Key Patterns

**Clean Architecture** — domain logic is fully isolated from infrastructure and UI.
Swapping JSON for SQL requires changing only the Infrastructure layer.

**Dependency Injection** — all dependencies are registered in a single `ServiceCollection`
in `Main()` and injected via constructors. No class instantiates its own dependencies.

**Repository Pattern** — UI classes depend on interfaces (`IBookRepository`, etc.),
never on concrete implementations.

**Service Layer** — `BookAvailabilityService` in the Application layer orchestrates
multiple repositories to answer a business question no single repository can answer alone.

**Immutable Transactions** — transaction records are write-only by design.
Compliance policy: no transaction is ever deleted. Reversals are recorded as new events.

---

## Availability Model

Book availability is not stored as a field. It is calculated at runtime: 

availableCopies = book.Quantity - active Borrow transactions

A Borrow transaction is considered active when no subsequent Return transaction exists
for the same member and book. This keeps the transaction log as the single source of
truth for the state of every copy in the library.

---

## Roadmap

- [ ] Unit tests for Domain entities and Application services
- [ ] Migrate persistence from JSON to SQL Server via Entity Framework Core
- [ ] Move to ASP.NET Core Web API
- [ ] HTML / CSS / JavaScript frontend

---

## Tech Stack

- C# / .NET 10
- `System.Text.Json` for serialization
- `Microsoft.Extensions.DependencyInjection` for DI container