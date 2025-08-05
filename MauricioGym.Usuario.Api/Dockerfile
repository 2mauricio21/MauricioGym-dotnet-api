FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MauricioGym.Usuario.API/MauricioGym.Usuario.API.csproj", "MauricioGym.Usuario.API/"]
COPY ["MauricioGym.Usuario/MauricioGym.Usuario.csproj", "MauricioGym.Usuario/"]
COPY ["MauricioGym.Infra/MauricioGym.Infra.csproj", "MauricioGym.Infra/"]
RUN dotnet restore "MauricioGym.Usuario.API/MauricioGym.Usuario.API.csproj"
COPY . .
WORKDIR "/src/MauricioGym.Usuario.API"
RUN dotnet build "MauricioGym.Usuario.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MauricioGym.Usuario.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MauricioGym.Usuario.API.dll"]