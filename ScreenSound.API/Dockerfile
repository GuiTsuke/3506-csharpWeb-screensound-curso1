# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia todos os arquivos do projeto
COPY . .

# Restaura as dependências
RUN dotnet restore "./ScreenSound.sln"

# Publica o projeto
RUN dotnet publish "./ScreenSound.API/ScreenSound.API.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
