# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KartoshkaEvent.Api/KartoshkaEvent.Api.csproj", "KartoshkaEvent.Api/"]
COPY ["KartoshkaEvent.Appication.Contracts/KartoshkaEvent.Application.Contracts.csproj", "KartoshkaEvent.Appication.Contracts/"]
COPY ["KartoshkaEvent.Application.Common/KartoshkaEvent.Application.Common.csproj", "KartoshkaEvent.Application.Common/"]
COPY ["KartoshkaEvent.Domain.Common/KartoshkaEvent.Domain.Common.csproj", "KartoshkaEvent.Domain.Common/"]
COPY ["KartoshkaEvent.Domain/KartoshkaEvent.Domain.csproj", "KartoshkaEvent.Domain/"]
COPY ["KartoshkaEvent.Appication/KartoshkaEvent.Application.csproj", "KartoshkaEvent.Appication/"]
COPY ["KartoshkaEvent.CacheService/KartoshkaEvent.CacheService.csproj", "KartoshkaEvent.CacheService/"]
COPY ["KartoshkaEvent.DataAccess/KartoshkaEvent.DataAccess.csproj", "KartoshkaEvent.DataAccess/"]
COPY ["KartoshkaEvent.JwtProvider/KartoshkaEvent.JwtProvider.csproj", "KartoshkaEvent.JwtProvider/"]
RUN dotnet restore "./KartoshkaEvent.Api/KartoshkaEvent.Api.csproj"
COPY . .
WORKDIR "/src/KartoshkaEvent.Api"
RUN dotnet build "./KartoshkaEvent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KartoshkaEvent.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KartoshkaEvent.Api.dll"]