FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["Bakery.Api/Bakery.Api.csproj", "Bakery.Api/"]
COPY ["../Bakery.Application/Bakery.Application.csproj", "../Bakery.Application/"]
COPY ["../Bakery.Infrastructure/Bakery.Infrastructure.csproj", "../Bakery.Infrastructure/"]
COPY ["../Bakery.Domain/Bakery.Domain.csproj", "../Bakery.Domain/"]

RUN dotnet restore "Bakery.Api/Bakery.Api.csproj"

COPY .. .

WORKDIR "/src/Bakery.Api"
RUN dotnet build "Bakery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Bakery.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bakery.Api.dll"]
