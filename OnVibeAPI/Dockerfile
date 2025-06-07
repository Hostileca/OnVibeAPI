FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnVibeAPI/OnVibeAPI.csproj", "OnVibeAPI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Contracts.DataAccess/Contracts.DataAccess.csproj", "Contracts.DataAccess/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.SignalR/Infrastructure.SignalR.csproj", "Infrastructure.SignalR/"]
COPY ["Contracts.Redis/Contracts.Redis.csproj", "Contracts.Redis/"]
COPY ["Contracts.SignalR/Contracts.SignalR.csproj", "Contracts.SignalR/"]
COPY ["Infrastructure.DataAccess/Infrastructure.DataAccess.csproj", "Infrastructure.DataAccess/"]
COPY ["Infrastructure.Hangfire/Infrastructure.Hangfire.csproj", "Infrastructure.Hangfire/"]
COPY ["Infrastructure.Redis/Infrastructure.Redis.csproj", "Infrastructure.Redis/"]
RUN dotnet restore "OnVibeAPI/OnVibeAPI.csproj"
COPY . .
WORKDIR "/src/OnVibeAPI"
RUN dotnet build "OnVibeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OnVibeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnVibeAPI.dll"]
