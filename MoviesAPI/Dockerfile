# Base image with .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["MoviesAPI.csproj", "."]
RUN dotnet restore "./MoviesAPI.csproj"

# Copy the entire project and build it
COPY . .
RUN dotnet build "./MoviesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "./MoviesAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image with runtime and published app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Ensure the app listens on all network interfaces
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "MoviesAPI.dll"]
