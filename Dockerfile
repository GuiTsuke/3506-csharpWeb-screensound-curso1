# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e restaura dependências
COPY . . 
RUN dotnet restore "./ScreenSound.sln"

# Build
RUN dotnet publish "./ScreenSound.API/ScreenSound.API.csproj" -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta 80, padrão do Render
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
