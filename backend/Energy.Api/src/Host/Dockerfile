# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5073
#EXPOSE 7271


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/Energy.Api/src/Host/Energy.Host.csproj", "backend/Energy.Api/src/Host/"]
COPY ["backend/Energy.Api/src/modules/Energy.Contracts/Energy.Contracts.csproj", "backend/Energy.Api/src/modules/Energy.Contracts/"]
COPY ["backend/Energy.Api/src/modules/Energy.Helpers/Energy.Helpers.csproj", "backend/Energy.Api/src/modules/Energy.Helpers/"]
RUN dotnet restore "./backend/Energy.Api/src/Host/Energy.Host.csproj"
COPY . .
WORKDIR "/src/backend/Energy.Api/src/Host"
RUN dotnet build "./Energy.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Energy.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY backend/Energy.Api/src/Host/appsettings*.json .
ENTRYPOINT ["dotnet", "Energy.Host.dll"]