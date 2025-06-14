FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file and project files
COPY Library.sln ./
COPY Library.Web/Library.Web.csproj Library.Web/

# Restore dependencies
RUN dotnet restore Library.Web/Library.Web.csproj

# Copy source code
COPY Library.Web/ Library.Web/

# Build and publish
WORKDIR /src/Library.Web
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Library.Web.dll"]
