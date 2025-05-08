FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file
COPY *.csproj .
RUN dotnet restore "Empetz_API.csproj"

# Copy everything else
COPY . .
RUN dotnet publish "Empetz_API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Empetz_API.dll"]