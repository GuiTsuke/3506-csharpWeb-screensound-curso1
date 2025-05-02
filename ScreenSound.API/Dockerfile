# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas o projeto da API
COPY . .    # ← Agora estamos dentro de ScreenSound.API
RUN dotnet restore "./ScreenSound.API.csproj"
RUN dotnet publish "./ScreenSound.API.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ScreenSound.API.dll"]
