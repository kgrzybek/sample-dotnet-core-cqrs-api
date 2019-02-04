Sample .NET Core REST API CQRS implementation with raw SQL and DDD.
==============================================================

Simple .NET Core REST API application implemented with basic CQRS approach.

Read Model - executing raw SQL scripts on database views objects (using Dapper).

Write Model - Domain Driven Design approach (using Entity Framework Core).

Commands/Queries/Domain Events handling using [MediatR](https://github.com/jbogard/MediatR) library.

See [blog post](http://www.kamilgrzybek.com/design/simple-cqrs-implementation-with-raw-sql-and-ddd/) about this solution.

### How to run
1. Create empty database.
2. Execute InitializeDatabase.sql script.
2. Set connection string (in appsettings.json or by user secrets mechanism).
3. Run!
