# ЭТАП 1: Сборка и восстановление зависимостей
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем файлы проектов для восстановления NuGet-пакетов
COPY ["ExamPreparationSystem.Web/ExamPreparationSystem.Web.csproj", "ExamPreparationSystem.Web/"]
COPY ["ExamPreparationSystem.Application/ExamPreparationSystem.Application.csproj", "ExamPreparationSystem.Application/"]
COPY ["ExamPreparationSystem.Domain/ExamPreparationSystem.Domain.csproj", "ExamPreparationSystem.Domain/"]

# Восстанавливаем зависимости
RUN dotnet restore "ExamPreparationSystem.Web/ExamPreparationSystem.Web.csproj"

# Копируем весь исходный код и собираем приложение
COPY . .
WORKDIR "/src/ExamPreparationSystem.Web"
RUN dotnet build "ExamPreparationSystem.Web.csproj" -c Release -o /app/build

# ЭТАП 2: Публикация готового релиза
FROM build AS publish
RUN dotnet publish "ExamPreparationSystem.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ЭТАП 3: Финальный минимальный образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем скомпилированные файлы из этапа публикации
COPY --from=publish /app/publish .

# Точка входа для запуска контейнера
ENTRYPOINT ["dotnet", "ExamPreparationSystem.Web.dll"]