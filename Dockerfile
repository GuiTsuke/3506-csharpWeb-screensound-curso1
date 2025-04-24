# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo de solução e restaurar dependências
COPY . .
RUN dotnet restore "./ScreenSound.sln"

# Compilar e publicar
RUN dotnet publish "./ScreenSound.API/ScreenSound.API.csproj" -c Release -o /src/ScreenSound.API/bin/Release/net8.0/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados
COPY --from=build /src/ScreenSound.API/bin/Release/net8.0/publish /app/publish

# Listar arquivos para diagnóstico
RUN ls -la /app/publish

# Expor a porta 80
EXPOSE 80

# Garantir permissões de execução
RUN chmod +x /app/publish/ScreenSound.API.dll

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "/app/publish/ScreenSound.API.dll"]
