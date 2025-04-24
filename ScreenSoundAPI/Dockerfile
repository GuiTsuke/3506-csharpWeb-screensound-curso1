# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY ScreenSound.Api/*.csproj ./ScreenSound.Api/
COPY ScreenSound.Shared/*.csproj ./ScreenSound.Shared/
COPY ScreenSound.Context/*.csproj ./ScreenSound.Context/

RUN dotnet restore

COPY . ./
WORKDIR /app/ScreenSound.Api
RUN dotnet publish -c Release -o out

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/ScreenSound.Api/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "ScreenSound.Api.dll"]
