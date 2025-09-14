# Берем образ .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Билдим
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем сам проект
COPY ["SimpleJira.Web/SimpleJira.Web.csproj", "SimpleJira.Web/"]

# Восстанавливаем зависимости
RUN dotnet restore "SimpleJira.Web/SimpleJira.Web.csproj"

# Копируем библиотеки и остальной код
COPY . .

# Собираем приложение
WORKDIR "/src/SimpleJira.Web"
RUN dotnet build -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "SimpleJira.Web.dll"]