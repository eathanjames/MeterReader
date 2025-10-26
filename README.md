# MeterReader

Seeding data, commands ran in Bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API


Initial Plan
- Implement a Clean Architecture layout with the following layers: API, Application, Domain, and Infrastructure.
- Start with Minimal APIs for quick endpoint scaffolding. Depending on progress and complexity, consider migrating to Controllers later
- Configure an in-memory SQLite database. Implement a data importer to seed the database on startup.
- Develop a CSV parser to process input files and map the data into DTOs for subsequent validation.
- Implement validation rules and corresponding unit tests to ensure data integrity.
Validation Rules:
- Duplicate entries must be rejected.
- Each meter reading must be linked to a valid Account ID.
- Reading values must follow the NNNNN numeric format.
- For accounts with existing readings, new readings must not be older than the latest recorded reading.



AI
Access to JetBrains AI and Chatgpt.
JetBrains AI used for autocompletion and checking primary unit test coverage.
Chatgpt used for sense checking and error debugging.
