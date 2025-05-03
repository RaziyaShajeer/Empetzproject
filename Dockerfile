# Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file from the nested directory
COPY ./Empetz_API/Empetz_API/*.csproj ./Empetz_API/Empetz_API/
RUN dotnet restore "./Empetz_API/Empetz_API/Empetz_API.csproj"

# Copy the rest of the files
COPY . .
RUN dotnet publish "./Empetz_API/Empetz_API/Empetz_API.csproj" -c Release -o /app

# Use the ASP.NET runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Empetz_API.dll"]