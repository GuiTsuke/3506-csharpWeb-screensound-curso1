# Etapa base: imagem para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expor a porta 80 (padrão para APIs no Render)
EXPOSE 80

# Etapa build: imagem para compilar e publicar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo de solução e restaurar dependências
COPY ScreenSound.sln ./
COPY ScreenSound.API/ScreenSound.API.csproj ./ScreenSound.API/
COPY ScreenSound.Shared.Data/ScreenSound.Shared.Data.csproj ./ScreenSound.Shared.Data/
COPY ScreenSound.Shared.Models/ScreenSound.Shared.Models.csproj ./ScreenSound.Shared.Models/

RUN dotnet restore

# Copiar o restante dos arquivos do projeto
COPY . ./

# Publicar a aplicação no diretório de saída
WORKDIR /src/ScreenSound.API
RUN dotnet publish -c Release -o /app/publish

# Etapa runtime: imagem para rodar a aplicação
FROM base AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
