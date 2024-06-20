# MyEmailProcessorProject

This project is a daemon application that processes emails by matching templates using both rule-based and ML.NET predictions.

## Project Structure

- **Data/**: Contains Entity Framework DbContext and entity classes.
- **Migrations/**: Contains Entity Framework migration files.
- **Models/**: Contains data models for ML.NET predictions.
- **Services/**: Contains service classes for email processing.
- **Templates/**: Contains email template classes.
- **MLModels/**: Contains classes and files related to ML.NET model.
- **appsettings.json**: Configuration file for application settings.
- **Program.cs**: Entry point for the application.
- **Startup.cs**: Configures services and the application's request pipeline.
- **README.md**: Project documentation.

## Getting Started

1. **Build the Project**:
    ```bash
    dotnet build
    ```

2. **Run Database Migrations**:
    ```bash
    dotnet ef database update
    ```

3. **Run the Application**:
    ```bash
    dotnet run
    ```

## Configuration

- **Database Connection**: Configure the connection string in `appsettings.json`.

## Dependencies

- .NET 6.0
- Entity Framework Core
- ML.NET