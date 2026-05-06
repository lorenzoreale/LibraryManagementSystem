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

- `Book` — entity with checkout and return logic
- `Member` — entity with email validation and registration date
- `Transaction` — immutable entity representing a borrow or return event
- `TransactionType` — enum (Borrow, Return)
- `IBookRepository` — contract for book persistence
- `IMemberRepository` — contract for member persistence
- `ITransactionRepository` — contract for transaction persistence

### LMS.Application
Intermediary layer between Domain and Infrastructure. Reserved for use cases and
orchestration logic as the project grows.

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
- Add a new book (with title and author validation)
- View all books with availability status
- Delete a book by ID

### Members
- Register a new member (with name, email, and date of birth)
- View all members
- Delete a member by ID

### Transactions *(in progress)*
- Record borrow and return events
- Query transactions by book or member

---

## Key Patterns

**Clean Architecture** — domain logic is fully isolated from infrastructure and UI.
Swapping JSON for SQL requires changing only the Infrastructure layer.

**Dependency Injection** — all dependencies are registered in a single `ServiceCollection`
in `Main()` and injected via constructors. No class instantiates its own dependencies.

**Repository Pattern** — UI classes depend on interfaces (`IBookRepository`, etc.),
never on concrete implementations.

**Immutable Transactions** — transaction records are write-only by design.
Compliance policy: no transaction is ever deleted. Reversals are recorded as new events.

---

## Roadmap

- [ ] Checkout flow (borrow a book as a member)
- [ ] Return flow
- [ ] Search and filter books / members
- [ ] Migrate persistence from JSON to SQL Server
- [ ] Move to ASP.NET Core Web API

---

## Tech Stack

- C# / .NET 10
- `System.Text.Json` for serialization
- `Microsoft.Extensions.DependencyInjection` for DI container