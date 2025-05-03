# Use a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem do SDK .NET para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copie o arquivo de projeto e restaure as dependências
COPY ["ScreenSound.Web/ScreenSound.Web.csproj", "ScreenSound.Web/"]
RUN dotnet restore "ScreenSound.Web/ScreenSound.Web.csproj"

# Copie todo o código e faça o build
COPY . .
WORKDIR "/src/ScreenSound.Web"
RUN dotnet publish "ScreenSound.Web.csproj" -c Release -o /app/publish

# Prepare a imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ScreenSound.Web.dll"]
