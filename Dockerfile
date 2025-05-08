FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file
COPY *.csproj .
RUN ls -l  # Debug: List files to confirm .csproj is copied
RUN dotnet restore "Empetz_API.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "Empetz_API.csproj" -c Release --no-restore
RUN dotnet publish "Empetz_API.csproj" -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Empetz_API.dll"]