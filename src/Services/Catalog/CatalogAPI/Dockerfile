#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# 1st layer : Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2nd layer : Build Operation
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/CatalogAPI/CatalogAPI.csproj", "Services/Catalog/CatalogAPI/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "./Services/Catalog/CatalogAPI/CatalogAPI.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogAPI"
RUN dotnet build "./CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3rd layer : Publish operation - compile the application and build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4th layer: Setup the final image and copying the publisehd application 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]