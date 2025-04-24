# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e restaura dependências
COPY . .
RUN dotnet restore "./ScreenSound.sln"

# Verificar se o build da API está sendo feito corretamente
RUN dotnet build "./ScreenSound.API/ScreenSound.API.csproj" -c Release

# Build e publicação
RUN dotnet publish "./ScreenSound.API/ScreenSound.API.csproj" -c Release -o /src/ScreenSound.API/bin/Release/net8.0/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /src/ScreenSound.API/bin/Release/net8.0/publish /app/publish

# Expor a porta 80 (padrão para APIs no Render)
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
