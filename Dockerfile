FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/Deliver.Api/Deliver.Api.csproj", "src/API/Deliver.Api/"]
COPY ["src/Core/Deliver.Application/Deliver.Application.csproj", "src/Core/Deliver.Application/"]
COPY ["src/Core/Deliver.Domain/Deliver.Domain.csproj", "src/Core/Deliver.Domain/"]
COPY ["src/Infrastructure/Deliver.Persistence/Deliver.Persistence.csproj", "src/Infrastructure/Deliver.Persistence/"]
RUN dotnet restore "./src/API/Deliver.Api/./Deliver.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Deliver.Api"
RUN dotnet build "./Deliver.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Deliver.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deliver.Api.dll"]