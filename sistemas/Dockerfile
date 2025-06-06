# 1. Imagen base para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2. Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /build

# Copiar solución y proyecto
COPY sistemas/sistemas.csproj sistemas/
RUN dotnet restore "sistemas/sistemas.csproj"

# Copiar el resto del código
COPY . .
WORKDIR /build/sistemas
RUN dotnet build "sistemas.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "sistemas.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# 4. Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sistemas.dll"]
