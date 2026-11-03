# Todo.Core

Простая .NET-библиотека для управления списком задач (Todo List).

## Возможности

- Добавление задач
- Удаление задач по ID
- Поиск задач по подстроке
- Отметка задачи как выполненной / невыполненной
- Переименование задач
- Сохранение и загрузка списка задач в/из JSON-файла

## Установка пакета через NuGet (GitHub Packages)

### 1. Добавить источник пакетов

```bash
dotnet nuget add source "https://nuget.pkg.github.com/<OWNER>/index.json" \
  --name "GitHubPackages" \
  --username <YOUR_GITHUB_USERNAME> \
  --password <YOUR_GITHUB_PAT>
```

> Замените `<OWNER>` на имя владельца репозитория, `<YOUR_GITHUB_USERNAME>` на ваш логин GitHub, `<YOUR_GITHUB_PAT>` на ваш Personal Access Token.

### 2. Установить пакет

```bash
dotnet add package Todo.Core --version 0.1.0
```

### 3. Использование в Visual Studio

1. **Tools → Options → NuGet Package Manager → Package Sources** — добавьте URL:
   `https://nuget.pkg.github.com/<OWNER>/index.json`
2. При установке пакета укажите учётные данные (username = ваш логин GitHub, password = PAT).
3. В **Package Manager → Browse** выберите источник **GitHubPackages** и установите пакет `Todo.Core`.

## Пример использования

```csharp
using Todo.Core;

var list = new TodoList();

// Добавить задачи
var task1 = list.Add("Купить молоко");
var task2 = list.Add("Прочитать книгу");

// Отметить выполненной
task1.MarkDone();

// Найти задачу
var found = list.Find("молоко");

// Сохранить в файл
list.Save("tasks.json");

// Загрузить из файла
var loaded = new TodoList();
loaded.Load("tasks.json");
```

## Сборка и тестирование

```bash
dotnet restore
dotnet build -c Release
dotnet test -c Release
```

## CI/CD

- **CI**: автоматическая сборка и тестирование при push/PR в ветку `master` (`.github/workflows/ci.yml`)
- **CD**: автоматическая публикация пакета в GitHub Packages после успешного CI (`.github/workflows/publish-package.yml`)
