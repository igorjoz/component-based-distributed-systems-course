FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and project files
COPY Library.sln ./
COPY Library.NotificationService2/Library.NotificationService2.csproj Library.NotificationService2/
COPY Library.WebApi/Library.WebApi.csproj Library.WebApi/

# Restore dependencies
RUN dotnet restore Library.NotificationService2/Library.NotificationService2.csproj

# Copy source code
COPY Library.NotificationService2/ Library.NotificationService2/
COPY Library.WebApi/ Library.WebApi/

# Build and publish
WORKDIR /src/Library.NotificationService2
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Library.NotificationService2.dll"]
