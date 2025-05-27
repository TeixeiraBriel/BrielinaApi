# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura pacotes do projeto principal
RUN dotnet restore Host/Host.csproj

# Publica o projeto principal
RUN dotnet publish Host/Host.csproj -c Release -o /app/publish --no-restore

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Define o ponto de entrada correto
ENTRYPOINT ["dotnet", "Host.dll"]
