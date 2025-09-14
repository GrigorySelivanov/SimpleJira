## Требования

Тестовое задание (Developer C# Senior)

Реализуйте backend-часть простой системы управления задачами (аналог Jira).
Основные требования:

Задачи:

- CRUD-операции.
- Поля: автор, исполнитель, статус (New, InProgress, Done), приоритет (Low, Medium, High).
- Вложенности: подзадачи и связи между задачами (“related to”).

Пользователи:

- Авторизация: прикрутить простой вариант JWT
- Регистрация/хранение пользователей не требуется.

БД:

- EF Core.
- MSSQL или PostgreSQL (на выбор).
- Миграции через EF Core.

Технические требования

- .NET 8, ASP.NET Core.
- Dependency Injection.
- CQRS (разделить команды/запросы).
- Swagger для документации API.
- Логирование (можно стандартное).
- docker-compose для запуска.

Будет плюсом:

- Unit-тесты на бизнес-логику.
- Чистая архитектура (или хотя бы Onion)

## Установка

### Вариант 1: Локальный запуск без Docker

1. Склонируйте репозиторий:

   ```bash
   git clone https://github.com/GrigorySelivanov/SimpleJira.git
   cd SimpleJira
   ```

2. Восстановите зависимости:

   ```bash
   dotnet restore
   ```

3. Соберите проект:

   ```bash
   dotnet build
   ```

4. Запустите приложение:

   ```bash
   cd SimpleJira.Web
   dotnet run
   ```

5. Откройте Swagger UI для тестирования API:
   ```
   https://localhost:7085/swagger
   ```

### Вариант 2: Запуск с использованием Docker

1. Убедитесь, что Docker и Docker Compose установлены на вашей системе.

2. Склонируйте репозиторий:

   ```bash
   git clone https://github.com/GrigorySelivanov/SimpleJira.git
   cd SimpleJira
   ```

3. Запустите приложение с помощью Docker Compose:

   ```bash
   docker-compose up --build
   ```

4. Откройте Swagger UI для тестирования API:

   ```
   http://localhost:8080/swagger
   ```

5. Для остановки контейнера используйте:
   ```bash
   docker-compose down
   ```
