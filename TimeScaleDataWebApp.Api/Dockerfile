FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5062

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["TimeScaleDataWebApp.Api/TimeScaleDataWebApp.Api.csproj", "TimeScaleDataWebApp.Api/"]
COPY ["TimeScaleDataWebApp.Application/TimeScaleDataWebApp.Application.csproj", "TimeScaleDataWebApp.Application/"]
COPY ["TimeScaleDataWebApp.Domain/TimeScaleDataWebApp.Domain.csproj", "TimeScaleDataWebApp.Domain/"]
COPY ["TimeScaleDataWebApp.Infrastructure/TimeScaleDataWebApp.Infrastructure.csproj", "TimeScaleDataWebApp.Infrastructure/"]

RUN dotnet restore "TimeScaleDataWebApp.Api/TimeScaleDataWebApp.Api.csproj"
COPY . .
WORKDIR "/src/TimeScaleDataWebApp.Api"

RUN dotnet build "./TimeScaleDataWebApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TimeScaleDataWebApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeScaleDataWebApp.Api.dll"]
