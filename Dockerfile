# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY EmailProcessingApi/*.csproj ./EmailProcessingApi/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/EmailProcessingApi
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EmailProcessingApi.dll"]