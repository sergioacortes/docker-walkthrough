FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DockerWalkThroguh.Api.Host/DockerWalkThroguh.Api.Host.csproj", "src/DockerWalkThroguh.Api.Host/"]
RUN dotnet restore "src/DockerWalkThroguh.Api.Host/DockerWalkThroguh.Api.Host.csproj"
COPY . .
WORKDIR "/src/src/DockerWalkThroguh.Api.Host"
RUN dotnet build "./DockerWalkThroguh.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DockerWalkThroguh.Api.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerWalkThroguh.Api.Host.dll"]
