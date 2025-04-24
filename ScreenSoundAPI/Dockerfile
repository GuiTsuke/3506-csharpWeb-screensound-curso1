# Etapa 1: Build do projeto
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ScreenSound.API/ScreenSound.API.csproj", "ScreenSound.API/"]
COPY ["ScreenSound.Shared/ScreenSound.Shared.csproj", "ScreenSound.Shared/"]
RUN dotnet restore "ScreenSound.API/ScreenSound.API.csproj"
COPY . .
WORKDIR "/src/ScreenSound.API"
RUN dotnet build "ScreenSound.API.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "ScreenSound.API.csproj" -c Release -o /app/publish

# Etapa 3: Rodar o aplicativo
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
