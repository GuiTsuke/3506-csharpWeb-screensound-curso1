# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia tudo (inclusive os projetos compartilhados)
COPY . .

# Restaura e publica a Web
WORKDIR /src/ScreenSound.Web
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ScreenSound.Web.dll"]
