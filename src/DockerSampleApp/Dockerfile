# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release
ARG GITVERSION=0.0.1
ARG MAJOR_MINOR_PATCH=0.0.1

COPY ["Directory.Build.props", "."]
COPY ["src/DockerSampleApp/DockerSampleApp.csproj", "src/DockerSampleApp/"]
RUN dotnet restore "./src/DockerSampleApp/DockerSampleApp.csproj"
COPY . .
WORKDIR "/src/src/DockerSampleApp"
RUN dotnet build "./DockerSampleApp.csproj" --no-restore -c ${BUILD_CONFIGURATION} /p:Version=$GITVERSION /p:FileVersion=$MAJOR_MINOR_PATCH /p:AssemblyVersion=$MAJOR_MINOR_PATCH


# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DockerSampleApp.csproj" --no-build -c ${BUILD_CONFIGURATION} -o /app/publish /p:Version=$GITVERSION /p:FileVersion=$MAJOR_MINOR_PATCH /p:AssemblyVersion=$MAJOR_MINOR_PATCH

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerSampleApp.dll"]